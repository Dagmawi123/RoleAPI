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
        public void NotifyChange(Role role);
        public Task<List<Role>> GetRoles();
        public Task<List<Role>> GetImmediateChildren(Guid id);
        public Task<Role> GetRole(Guid id);
        public Task<bool> CheckRole(Guid id);
    }
}
