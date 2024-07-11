using Microsoft.EntityFrameworkCore;
using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public class RoleQueriesRepository : IRoleQueriesRepository
    {
        RoleContext _context;
        public RoleQueriesRepository(RoleContext context)
        {
            _context = context;
        }

        public Role checkRole(Guid? id)
        {
            Role? role = _context.Roles.Where(r => r.Id == id).Include(r => r.Parent).FirstOrDefault();
            if (role == null)
            {
                return null;
            }
            else
                return role;

        }

        public Role GetRole(Guid id)
        {
            Role? role = _context.Roles.Where(r => r.Id == id).Include(r => r.Parent).FirstOrDefault();
            return role;
        }


        public void findChildren(Guid id, List<Role> roles, List<Role> _roles)
        {
            List<Role> childRoles = _roles
                 .Where(r => r.Parent?.Id == id)
                 .ToList();
            roles.AddRange(childRoles);
            foreach (Role role in childRoles)
            {
                findChildren(role.Id, roles, _roles);
            }
            return;

        }

        public List<Role> Roles()
        {
            return _context.Roles.Include(r => r.Parent).ToList();

        }

        public string GetTree()
        {
            List<Role> roles = Roles();
            Guid Id = roles.Where(r => r.Name == "CEO").Select(r => r.Id).First();
            string tree = DrawTree("", "", Id, roles);
            return tree;
        }



        public string DrawTree(string link, string indent, Guid Id, List<Role> roles)
        {

            string line = indent + link + roles.Where(r => r.Id == Id).Select(r => r.Name).First();
            line += "\n" + "│";
            // retrieve all children
            List<Role> ChildRoles = roles.Where(r => r.Parent != null && r.Parent.Id == Id).ToList();
            foreach (Role ChildRole in ChildRoles)
            {
                line += DrawTree("├", indent + "─", ChildRole.Id, roles);
            }
            return line;

        }



    }
}
