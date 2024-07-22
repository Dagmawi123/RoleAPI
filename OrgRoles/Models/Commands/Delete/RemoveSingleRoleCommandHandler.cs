
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace OrgRoles.Models.Commands.Delete
{
    public class RemoveSingleRoleCommandHandler(RoleContext context) : IRequestHandler<RemoveSingleRoleCommand>
    {
        public async Task Handle(RemoveSingleRoleCommand rrc, CancellationToken token)
        {
            List<Role> childRoles = context.Roles
                                      .Where(r => (r.Parent != null && r.Parent.Id == rrc.role.Id))
                                      .ToList();
            foreach (Role childRole in childRoles)
            {
                childRole.Parent = rrc.role.Parent;
            }
            context.Roles.Remove(rrc.role);
            await context.SaveChangesAsync();
        }
    }
}
