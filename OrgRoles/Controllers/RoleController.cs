using Microsoft.AspNetCore.Mvc;
using OrgRoles.Models;
using OrgRoles.Models.Commands;
using OrgRoles.Models.Repos;

namespace OrgRoles.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        IRoleRepository roleRepo;
        //IRoleCommandsRepository commandsRepo;
        //IRoleQueriesRepository queriesRepo;

       private readonly IRoleCommands roleCommands;
        private readonly IRoleQueries roleQueries;

        public RoleController(IRoleCommands roleCommands,IRoleQueries roleQueries, IRoleRepository roleRepo)
        {
            //this.roleRepo = roleRepo;
            //this.queriesRepo = queriesRepo;
            //this.commandsRepo= commandsRepo;
            this.roleQueries = roleQueries;
            this.roleCommands = roleCommands;
            this.roleRepo = roleRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(RoleRequest roleRq)
        {

            Role _role = await roleCommands.SaveRole(roleRq);
            return CreatedAtAction(nameof(CreateRole), _role);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(Guid id, RoleRequest roleRq)
        {
            Role? role =roleQueries.checkRole(id);
            if (role == null)
            {
                return NotFound();
            }
            role = await roleCommands.UpdateRole(role, roleRq);
            return Ok(role);
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetRole(Guid id)
        {
            Role role = roleQueries.GetRole(id);
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
               await roleRepo.RemoveRole(role);
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
                await roleRepo.RemoveThisRole(role);
                return NoContent();
            }

        }




        [Route("~/api/roles/allChildren/{id}")]
        [HttpGet]
        public ActionResult<List<Role>> GetChildren(Guid id)
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
            return Ok(children);
            }
            
        }


       [HttpGet]
       public ActionResult<List<Role>> GetRoles() {
         string tree = roleQueries.GetTree();
              return Ok(tree);
            
        }


        }
}
