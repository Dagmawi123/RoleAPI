namespace OrgRoles.Models.Commands
{
    public interface IRoleCommands
    {
        public Task<Role> SaveRole(RoleRequest roleRq);
        public  Task<Role> UpdateRole(Role role, RoleRequest roleRq);


    }
}
