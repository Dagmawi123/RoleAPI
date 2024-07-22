using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OrgRoles.Models.Queries.Get
{
    public class GetChildrenQueryHandler(RoleContext context,IConfiguration configuration) : IRequestHandler<GetChildrenQuery>
    {
        public async Task Handle(GetChildrenQuery gcq,CancellationToken token) 
        {
            List<Role> children=new();
           await findChildren(gcq.Id, gcq.children, gcq._roles);
        
        }

        public async Task findChildren(Guid id, List<Role> roles, List<Role> _roles)
        {
            List<Role> childRoles = _roles
                 .Where(r => r.Parent?.Id == id)
                 .ToList();
            roles.AddRange(childRoles);
            foreach (Role role in childRoles)
            {
               await findChildren(role.Id, roles, _roles);
            }
            return;

        }

    }
}
