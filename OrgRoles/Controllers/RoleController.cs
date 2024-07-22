using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgRoles.Models;
using OrgRoles.Models.Commands.Create;
using OrgRoles.Models.Commands.Delete;
using OrgRoles.Models.Commands.Old;
using OrgRoles.Models.Commands.Update;
using OrgRoles.Models.Queries;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;

namespace OrgRoles.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {       

       private readonly IRoleCommands roleCommands;
        private readonly IRoleQueries roleQueries;
        private readonly IMediator mediator;

        public RoleController(IRoleCommands roleCommands,IRoleQueries roleQueries, IMediator mediator)
        {
       
            this.roleQueries = roleQueries;
            this.roleCommands = roleCommands;
            this.mediator = mediator;
   
        }



        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(RoleDTO roleRq)
        {
            var command = new AddRoleCommand(roleRq);

            //Role _role = await roleCommands.SaveRole(roleRq);
            Role _role = await mediator.Send(command);

            return CreatedAtAction(nameof(CreateRole), _role);
        }
        


        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(Guid id, RoleDTO roleRq)
        {
            Role? role =roleQueries.checkRole(id);
            if (role == null)
            {
                return NotFound();
            }
            var command = new UpdateRoleCommand(role,roleRq);
            role=await mediator.Send(command);

           // role = await roleCommands.UpdateRole(role, roleRq);
            return Ok(role);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(Guid id)
        {
            var query = new GetRoleQuery(id);
          Role role = await mediator.Send(query);
           //  role = await roleQueries.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            else
                return Ok(role);

        }

        //removing including all children 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> RemoveRole(Guid id) {
            Role? role = roleQueries.checkRole(id);
            if (role == null)
            {
                return NotFound();
            }
            else {
                var command = new RemoveRoleCommand(role);
                await mediator.Send(command);
             //  await roleCommands.RemoveRole(role);
            }

                return NoContent();
                
        }

        //removing only the given node -assigning children nodes into parent
         [Route("~/api/roles/removeThis/{id}")]
        [HttpDelete]
         public async Task<ActionResult<Role>> RemoveSingleRole(Guid id)
        {
            Role? role = roleQueries.checkRole(id);
            if (role == null)
            {
                return NotFound();
            }
            else {
                //await roleCommands.RemoveThisRole(role);
                var command = new RemoveSingleRoleCommand(role);
                await mediator.Send(command);

                return NoContent();
            }

        }




        [Route("~/api/roles/allChildren/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetChildren(Guid id)
        {
            List<Role> _roles= roleQueries.Roles();
            Role? role = _roles.Find(r=>r.Id==id);
            if (role == null)
            {
                return NotFound();
            }
            else { 
            List<Role> children = new List<Role>();
                roleQueries.findChildren(role.Id, children,_roles);

                var query = new GetChildrenQuery(role.Id,children,_roles);
                await mediator.Send(query);


                return Ok(children);
            }
            
        }


       [HttpGet]
       public async Task<ActionResult<string>> GetRoles() {
            var query= new GetRolesQuery();
            string tree=await mediator.Send(query);
         //string tree = roleQueries.GetTree();
              return Ok(tree);
            
        }


        }
}
