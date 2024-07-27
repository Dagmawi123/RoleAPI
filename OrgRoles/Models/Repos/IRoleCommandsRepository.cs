using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public interface IRoleCommandsRepository
    {
        public Task<Role> AddRole(Role role);
        public Task UpdateRole(Role role);
        public void RemoveRole(Role role);
        public Task SaveChanges();
       // public void RemoveRecursive(Role role);
        public Task RemoveThisRole(Role role);

    }
}
