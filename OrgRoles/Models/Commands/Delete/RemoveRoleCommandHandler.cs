using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OrgRoles.Models.Commands.Delete
{
    public class RemoveRoleCommandHandler(RoleContext context) : IRequestHandler<RemoveRoleCommand> 
    {

        public async Task Handle(RemoveRoleCommand rrc,CancellationToken token) 
        { 
         RemoveRecursive(rrc.role);
            await context.SaveChangesAsync();
            return;
        }

        public void RemoveRecursive(Role role)
        {

            List<Role> childRoles = context.Roles
               .Where(r => r.Parent != null && r.Parent.Id == role.Id)
               .ToList();

            foreach (Role ChildRole in childRoles)
            {
                RemoveRecursive(ChildRole);
            }
            context.Roles.Remove(role);

        }
    }
}
