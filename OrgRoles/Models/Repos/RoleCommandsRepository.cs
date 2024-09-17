using Microsoft.EntityFrameworkCore;
using OrgRoles.Models;
using Dapper;
using System.Data.SqlClient;
using OrgRoles.Configuration;

namespace OrgRoles.Models.Repos
{
    public class RoleCommandsRepository : IRoleCommandsRepository
    {
        private readonly RoleContext _context;
        private readonly IConfiguration configuration;
        public RoleCommandsRepository(RoleContext context,IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
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



        public async Task<List<Role>> GetRoles()
        {
            var sql = "select * from public." + '"' + "Roles" + '"';
            var PGSQL = new PGSQL(configuration);
            using (var connection = PGSQL.createConnection())
            {
                connection.Open();
                var roless = await connection.QueryAsync<Role>(sql);
                connection.Close();
                return roless.ToList();

            }
        }

        public async Task<List<Role>> GetImmediateChildren(Guid id)
        {
            var PGSQL = new PGSQL(configuration);
            using (var connection = PGSQL.createConnection())
            {
                connection.Open();
                var query = "select * from public. " + '"' + "Roles" + '"' + "where"+'"'+"ParentId"+'"'+"=@id";
                var results = await connection.QueryAsync<Role>(query, new { id });
                connection.Close();
                return results.ToList();
            
            }

        }

        public async Task<Role> GetRole(Guid id)
        {
            Role? role;
            var sql = "select * from public." + '"' + "Roles" + '"' + "where Id= @id";
            var PGSQL = new PGSQL(configuration);
            using (var connection = PGSQL.createConnection())
            {
                connection.Open();
                role = await connection.QuerySingleOrDefaultAsync<Role>(sql, new { id });
                connection.Close();

            }
            return role;
        }

        public async Task<bool> CheckRole(Guid id)
        {
            // if (id == null) return false;
            Role role = await GetRole(id);
            if (role == null) return false;
            return true;
        }

    }
}
