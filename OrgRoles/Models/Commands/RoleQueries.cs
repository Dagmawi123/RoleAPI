using OrgRoles.Models.Repos;
using System.Data;

namespace OrgRoles.Models.Commands
{
    public class RoleQueries:IRoleQueries
    {
        IRoleQueriesRepository queriesRepository;


        public RoleQueries(IRoleQueriesRepository queriesRepository)
        {

            this.queriesRepository = queriesRepository;
        }
        public Role checkRole(Guid? id) {
            Role? role = queriesRepository.checkRole(id);
            return role;
        }

        public Role GetRole(Guid id)
        {
            Role role = queriesRepository.GetRole(id);
            return role;
        }

        public List<Role> Roles()
        {
          List<Role> _roles = queriesRepository.Roles();
            return _roles;
        }

        public void findChildren(Guid id, List<Role> roles, List<Role> _roles)
        {
            queriesRepository.findChildren(id, roles, _roles); 
        }

        public string GetTree()
        {
            string tree = queriesRepository.GetTree();
            return tree;
        }
    
    }
}
