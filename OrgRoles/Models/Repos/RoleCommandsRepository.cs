using Microsoft.EntityFrameworkCore;
using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public class RoleCommandsRepository : IRoleCommandsRepository
    {
        private readonly RoleContext _context;
        public RoleCommandsRepository(RoleContext context)
        {
            _context = context;
        }


        public async Task<Role> AddRole(RoleDTO roleRq)
        {
            Role _role = new()
            {
                Name = roleRq.Name,
                Description = roleRq.Description,
            };
            if (roleRq.ParentID.HasValue)
            {
                _role.ParentId = _context.Roles.Where(r => r.Id == roleRq.ParentID).Select(r => r.Id).FirstOrDefault();
            }
            
            _context.Roles.Add(_role);
                await _context.SaveChangesAsync();
            return _role;


        }

        public async Task<Role> UpdateRole(Role role, RoleDTO roleRq)
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

        public async Task RemoveRole(Role role)
        {
            RemoveRecursive(role);
            await _context.SaveChangesAsync();
            return;
        }

        public void RemoveRecursive(Role role)
        {

            List<Role> childRoles = _context.Roles
               .Where(r => r.Parent != null && r.Parent.Id == role.Id)
               .ToList();

            foreach (Role ChildRole in childRoles)
            {
                RemoveRecursive(ChildRole);
            }
            _context.Roles.Remove(role);

        }


        public async Task RemoveThisRole(Role role)
        {

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







    }
}
