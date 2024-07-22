using OrgRoles.Models.Repos;
using System.Data;

namespace OrgRoles.Models.Commands.Old
{
    public class RoleCommands : IRoleCommands
    {

        private readonly IRoleCommandsRepository commandsRepo;


        public RoleCommands(IRoleCommandsRepository commandsRepo)
        {

            this.commandsRepo = commandsRepo;
        }

        public async Task<Role> SaveRole(RoleDTO roleRq)
        {

            Role _role = await commandsRepo.AddRole(roleRq);
            return _role;
        }

        public async Task<Role> UpdateRole(Role role, RoleDTO roleRq)
        {
            role = await commandsRepo.UpdateRole(role, roleRq);
            return role;
        }

        public async Task RemoveRole(Role role)
        {

            await commandsRepo.RemoveRole(role);
        }

        public async Task RemoveThisRole(Role role)
        {
            await commandsRepo.RemoveThisRole(role);
        }





    }
}
