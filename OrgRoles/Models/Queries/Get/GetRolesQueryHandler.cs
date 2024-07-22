using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OrgRoles.Models.Queries.Get
{
    public class GetRolesQueryHandler(RoleContext context) : IRequestHandler<GetRolesQuery ,string>
    {
        public async Task<string> Handle(GetRolesQuery grq,CancellationToken token) 
        {
            List<Role> roles = await context.Roles.Include(r => r.Parent).ToListAsync();
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
