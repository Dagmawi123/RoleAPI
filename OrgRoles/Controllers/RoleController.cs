using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgRoles.Models;
using OrgRoles.Models.Commands.Create;
using OrgRoles.Models.Commands.Delete;
using OrgRoles.Models.Commands.Update;
using OrgRoles.Models.Queries.Get;

namespace OrgRoles.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {       

      
        private readonly IMediator mediator;
        private readonly IGetRepository getRepository;

        public RoleController(IMediator mediator,IGetRepository getRepository)
        {
            this.mediator = mediator;
            this.getRepository = getRepository;
        }



        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(AddRoleCommand command)
        {
                 Role _role = await mediator.Send(command);
                 return CreatedAtAction(nameof(CreateRole), _role);
        }
        

        //accept the command directly from the API
        //no mediator for get methods rather some repository 
        //use repositories for immediate effect adding,removing,updating
        //validation
        //one more repo for dapper-getting
        //role priority?
        //snail case for columns..
        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(UpdateRoleCommand command)
        {
           
            Role role=await mediator.Send(command);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
          
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(Guid id)
        {
            try
            {
             
                Role role = await getRepository.GetRole(id);
                if (role == null)
                {
                    return NotFound();
                }
                else
                    return Ok(role);
            }
            catch (Exception e) {
              Console.WriteLine(e.Message);
                return Ok();   
            }
        }

        //removing including all children 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> RemoveRole(Guid id) {
            Role? role = await getRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            else {
                var command = new RemoveRoleCommand(role);
                await mediator.Send(command);
            }

                return NoContent();
                
        }

        //removing only the given node -assigning children nodes into parent
         [Route("~/api/roles/removeThis/{id}")]
        [HttpDelete]
         public async Task<ActionResult<Role>> RemoveSingleRole(Guid id)
        {
            Role? role =await getRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            else {
                var command = new RemoveSingleRoleCommand(role);
                await mediator.Send(command);

                return NoContent();
            }

        }




        [Route("~/api/roles/allChildren/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetChildren(Guid id)
        {
            
            List<Role> children = await getRepository.GetChildren(id);
            if (children == null)
                return NotFound();
            else if (children.Count() == 0)
                return NotFound("Unfortunately there are no children for this role😥");
            else
                return Ok(children);
            
            
        }


       [HttpGet]
       public async Task<ActionResult<List<Role>>> GetRoles() 
        {
          
         List<Role> roles = await getRepository.GetRoles();
              return Ok(roles);
            
        }


        }
}
