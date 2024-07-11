using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace OrgRoles.Models
{
    public class RoleRepository : IRoleRepository
    {
        RoleContext _context;
        public RoleRepository(RoleContext context)
        {
            _context = context;
        }

        public async Task<Role>  AddRole(RoleRequest roleRq)
        {
            Role _role = new Role()
            {
                Name = roleRq.Name,
                Description = roleRq.Description,
            };
            if (roleRq.ParentID.HasValue)
            {
                _role.ParentId = _context.Roles.Where(r => r.Id == roleRq.ParentID).Select(r => r.Id).FirstOrDefault();
            }
                //  should checkRole be in read or write repository ??

                //Role role = checkRole(roleRq.ParentID);
                //if (role != null) {
                //    _role.ParentId = role.ParentId;
                //}
          
            _context.Roles.Add(_role);
            await _context.SaveChangesAsync();
            return _role;


        }
        public Role checkRole(Guid? id)
        {
            Role? role = _context.Roles.Where(r => r.Id == id).Include(r => r.Parent).FirstOrDefault();
            if (role == null)
            {
                return null;
            }
            else   return role;

        }

        public async Task<Role> UpdateRole(Role role,RoleRequest roleRq) 
        {
            role.Name = roleRq.Name;
            role.Description = roleRq.Description;
            if (roleRq.ParentID.HasValue)
            {
              role.ParentId = _context.Roles.Where(r => r.Id == roleRq.ParentID).Select(r => r.Id).FirstOrDefault();
            }
            await _context.SaveChangesAsync();
            return role;
        }

        public Role GetRole(Guid id)
        {
            Role? role = _context.Roles.Where(r => r.Id == id).Include(r => r.Parent).FirstOrDefault();
            return role;
        }

        //ON DELETE CASCADE deletion



        public async Task RemoveRole(Role role) 
        {
            RemoveRecursive(role);
            await _context.SaveChangesAsync();
               return;
        }

        public void RemoveRecursive(Role role) {

            List<Role> childRoles = _context.Roles
               .Where(r => r.Parent != null && r.Parent.Id == role.Id)
               .ToList();

            foreach (Role ChildRole in childRoles)
            {
                RemoveRecursive(ChildRole);
            }
            _context.Roles.Remove(role);

        }


        public async Task RemoveThisRole(Role role) {

            List<Role> childRoles = _context.Roles
                            .Where(r => (r.Parent != null && r.Parent.Id == role.Id))
                            .ToList();
            foreach (Role childRole in childRoles)
            {
                childRole.Parent = role.Parent;
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

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

        public string GetTree() {
            List<Role> roles = Roles();
            Guid Id = roles.Where(r => r.Name=="CEO").Select(r => r.Id).First();
            string tree = DrawTree("", "", Id,roles );
            return tree;
        }

        

        public string DrawTree(String link, string indent, Guid Id, List<Role> roles)
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
