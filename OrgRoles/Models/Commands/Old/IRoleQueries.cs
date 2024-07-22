namespace OrgRoles.Models.Commands.Old
{
    public interface IRoleQueries
    {
        public Role checkRole(Guid? id);
        //  public Role GetRole(Guid id);
        public List<Role> Roles();
        public void findChildren(Guid id, List<Role> roles, List<Role> _roles);
        public string GetTree();
        public Task<Role> GetRole(Guid id);
    }
}
