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


        public async Task<Role> AddRole(Role role)
        {
            
            _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            return role;


        }

        public async Task UpdateRole(Role role)
        {   _context.Entry(role).State = EntityState.Modified;
            await SaveChanges();          
        }
        public async Task SaveChanges() {
            await _context.SaveChangesAsync(); 
        }
        public void NotifyChange(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
        }

        public void RemoveRole(Role role)
        {
         
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
