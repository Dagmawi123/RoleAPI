using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public interface IRoleCommandsRepository
    {
        public Task<Role> AddRole(RoleDTO roleRq);
        public Task<Role> UpdateRole(Role role, RoleDTO roleRq);
        public Task RemoveRole(Role role);
        public void RemoveRecursive(Role role);
        public Task RemoveThisRole(Role role);

    }
}
