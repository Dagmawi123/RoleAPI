namespace OrgRoles.Models
{
    public interface IRoleRepository 
    {
        public  Task<Role> AddRole(RoleRequest roleRq);
        public Role checkRole(Guid id);
        public  Task<Role> UpdateRole(Role role, RoleRequest roleRq);
        public Role GetRole(Guid id);
        public  Task RemoveRole(Role role);
        public Task RemoveThisRole(Role role);
        public void findChildren(Guid id, List<Role> roles, List<Role> _roles);
        public List<Role> Roles();
        public string GetTree();
        public string DrawTree(String link, string indent, Guid Id, List<Role> roles);

    }
}
