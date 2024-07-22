using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace OrgRoles.Models.Queries.Get
{
    public class GetRoleQueryHandler(RoleContext context, IConfiguration configuration) : IRequestHandler<GetRoleQuery, Role>
    {
        public async Task<Role> Handle(GetRoleQuery getRoleQuery, CancellationToken token)
        {
            Role? role;
            var sql = "select * from Roles where Id= @id";
            using (var myconnection = new SqlConnection(configuration.GetConnectionString("MyConnection")))
            {
                myconnection.Open();
                role = await myconnection.QuerySingleOrDefaultAsync<Role>(sql, new { id = getRoleQuery.Id });


            }
            return role;
        }
    }
}
