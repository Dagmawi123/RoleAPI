using OrgRoles.Models.Repos;

namespace OrgRoles.Models.Commands
{
    public class RoleCommands:IRoleCommands
    {
      
        IRoleCommandsRepository commandsRepo;


        public RoleCommands(IRoleCommandsRepository commandsRepo)
        {

            this.commandsRepo = commandsRepo;
        }

        public async Task<Role> SaveRole(RoleRequest roleRq) {

            Role _role = await commandsRepo.AddRole(roleRq);
            return _role;
        }

        public async Task<Role> UpdateRole(Role role, RoleRequest roleRq) 
        {
            role = await commandsRepo.UpdateRole(role, roleRq); 
            return role;
        }

     


    }
}
