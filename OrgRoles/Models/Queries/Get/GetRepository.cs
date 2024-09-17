using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Configuration;

namespace OrgRoles.Models.Queries.Get
{
    public class GetRepository(IConfiguration configuration) : IGetRepository
    {
        public async Task<Role> GetRole(Guid id) {
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
        public async Task<List<Role>> GetChildren(Guid id) {
            var PGSQL = new PGSQL(configuration);
            using (var connection = PGSQL.createConnection())
            {
                connection.Open();
                var query = "select * from public." + '"' + "Roles" + '"'+";" +
                    "select * from public."+ '"' + "Roles" + '"' +
                    " where Id in (select distinct "+'"'+"ParentId"+'"' +"from public."+'"'+"Roles"+'"'+")";
               // var query =@"select * from Roles; select * from Roles where Id in (select distinct ParentId from Roles ) ";
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
                connection.Close();
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
            var sql = "select * from public." + '"' + "Roles" + '"';
            var PGSQL = new PGSQL(configuration);
            using (var connection = PGSQL.createConnection())
            {
              //  List<Role> roles = con.Roles.ToList();
                connection.Open();
                var roless = await connection.QueryAsync<Role>(sql);
                 connection.Close();
                return roless.ToList();
               

            }

        }
        
      }
}
