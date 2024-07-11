using Microsoft.EntityFrameworkCore;
using OrgRoles.Models;

namespace OrgRoles.Models.Repos
{
    public class RoleCommandsRepository : IRoleCommandsRepository
    {
        RoleContext _context;
        public RoleCommandsRepository(RoleContext context)
        {
            _context = context;
        }


        public async Task<Role> AddRole(RoleRequest roleRq)
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

        public async Task<Role> UpdateRole(Role role, RoleRequest roleRq)
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






    }
}
