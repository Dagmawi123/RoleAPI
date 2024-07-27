using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System;
using Microsoft.EntityFrameworkCore;

namespace OrgRoles.Models.Queries.Get
{
    public class GetRepository(IConfiguration configuration) : IGetRepository
    {
        public async Task<Role> GetRole(Guid id) {
            Role? role;
            var sql = "select * from Roles where Id= @id";
            using (var myconnection = new SqlConnection(configuration.GetConnectionString("MyConnection")))
            {
                myconnection.Open();
                role = await myconnection.QuerySingleOrDefaultAsync<Role>(sql, new { id });


            }
            return role;
        }
        public async Task<List<Role>> GetChildren(Guid id) {
            using (var connection =new SqlConnection(configuration.GetConnectionString("MyConnection"))) {
                connection.Open();
                var query =@"select * from Roles; select * from Roles where Id in (select distinct ParentId from Roles ) ";
              var multi= connection.QueryMultiple(query);
                var roles = multi.Read<Role>().ToList();
                var parents = multi.Read<Role>().ToList();
                foreach (var rol in roles) {
                    rol.Parent = parents.FirstOrDefault(p=>p.Id==rol.ParentId);
                }

            Role? role = roles.Where(r => r.Id == id).FirstOrDefault();
            if (role == null)
                return null;
            List<Role> children = new();
            await findChildren(id, children, roles);
            return children;
        }

           
        }

                //List<Role> roles = context.Roles.Include(r => r.Parent).ToList();
           

        public async Task findChildren(Guid id, List<Role> roles, List<Role> _roles)
        {
            List<Role> childRoles = _roles
                 .Where(r => r.ParentId == id)
                 .ToList();
            roles.AddRange(childRoles);
            foreach (Role role in childRoles)
            {
                await findChildren(role.Id, roles, _roles);
            }
            return;

        }


        public async Task<List<Role>> GetRoles() {
            var sql = "select * from Roles";
            using (var myconnection = new SqlConnection(configuration.GetConnectionString("MyConnection")))
            {
                myconnection.Open();
                var roless = await myconnection.QueryAsync<Role>(sql);
                return roless.ToList();

            }
        }
        public async Task<bool> CheckRole(Guid id) {
           // if (id == null) return false;
            Role role = await GetRole(id);
            if (role == null) return false;
            return true;
        }
    }


}
