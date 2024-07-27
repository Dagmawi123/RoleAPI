namespace OrgRoles.Models.Queries.Get
{
    public interface IGetRepository
    {
        public Task<Role> GetRole(Guid id);
        public Task<List<Role>> GetChildren(Guid id);
        public Task<List<Role>> GetRoles();
        public Task<bool> CheckRole(Guid id);

    }
}
