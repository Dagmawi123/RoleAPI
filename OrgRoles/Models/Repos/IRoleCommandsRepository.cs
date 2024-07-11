using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public interface IRoleCommandsRepository
    {
        public Task<Role> AddRole(RoleRequest roleRq);
        public Task<Role> UpdateRole(Role role, RoleRequest roleRq);

    }
}
