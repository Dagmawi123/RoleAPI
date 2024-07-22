using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public interface IRoleQueriesRepository
    {
        public Role checkRole(Guid? id);
        //public Role GetRole(Guid id);
        public void findChildren(Guid id, List<Role> roles, List<Role> _roles);
        public List<Role> Roles();
        public string GetTree();
        public string DrawTree(string link, string indent, Guid Id, List<Role> roles);
        public Task<Role> GetRole(Guid id);
    }
}
