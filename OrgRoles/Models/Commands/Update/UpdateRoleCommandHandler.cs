using MediatR;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Commands.Create;
using System.Data;

namespace OrgRoles.Models.Commands.Update
{
    public class UpdateRoleCommandHandler(RoleContext context) : IRequestHandler<UpdateRoleCommand, Role>
    {
        public async Task<Role> Handle(UpdateRoleCommand updateRolecommand, CancellationToken token)
        {
            updateRolecommand.role.Name = updateRolecommand.rdto.Name;
            updateRolecommand.role.Description = updateRolecommand.rdto.Description;
            if (updateRolecommand.rdto.ParentID.HasValue)
            {
                updateRolecommand.role.ParentId = context.Roles.Where(r => r.Id == updateRolecommand.rdto.ParentID).Select(r => r.Id).FirstOrDefault();
            }
            await context.SaveChangesAsync();
            return updateRolecommand.role;
        }
    
    }

}
