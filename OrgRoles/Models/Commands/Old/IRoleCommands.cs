namespace OrgRoles.Models.Commands.Old
{
    public interface IRoleCommands
    {
        public Task<Role> SaveRole(RoleDTO roleRq);
        public Task<Role> UpdateRole(Role role, RoleDTO roleRq);
        public Task RemoveRole(Role role);
        public Task RemoveThisRole(Role role);

    }
}
