using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrgRoles.Models;
using System.Data;

namespace OrgRoles.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        IRoleRepository roleRepo;
        public RoleController(RoleContext roleContext, IRoleRepository roleRepo)
        {
            this.roleRepo = roleRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(RoleRequest roleRq)
        {
            Role _role = await roleRepo.AddRole(roleRq);
            return CreatedAtAction(nameof(CreateRole), _role);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(Guid id, RoleRequest roleRq)
        {
            Role? role =roleRepo.checkRole(id);
            if (role == null)
            {
                return NotFound();
            }
            role = await roleRepo.UpdateRole(role, roleRq);
            return Ok(role);
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetRole(Guid id)
        {
            Role role = roleRepo.GetRole(id);
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
            Role? role = roleRepo.checkRole(id);
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
            Role? role = roleRepo.checkRole(id);
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
            List<Role> _roles= roleRepo.Roles();
            Role? role = _roles.Find(r=>r.Id==id);
            if (role == null)
            {
                return NotFound();
            }
            else { 
            List<Role> children = new List<Role>();
                roleRepo.findChildren(role.Id, children,_roles);
            return Ok(children);
            }
            
        }


       [HttpGet]
       public ActionResult<List<Role>> GetRoles() {
         string tree = roleRepo.GetTree();
              return Ok(tree);
            
        }


        }
}
