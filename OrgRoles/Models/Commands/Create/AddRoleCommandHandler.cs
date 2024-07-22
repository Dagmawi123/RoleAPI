using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OrgRoles.Models.Commands.Create
{
    public class AddRoleCommandHandler(RoleContext context) :IRequestHandler<AddRoleCommand,Role>
    {
        public async Task<Role> Handle(AddRoleCommand ALC , CancellationToken token) {

            Role _role = new()
            {
                Name = ALC.rdto.Name,
                Description = ALC.rdto.Description,
            };
            if (ALC.rdto.ParentID.HasValue)
            {
                _role.ParentId = context.Roles.Where(r => r.Id == (ALC.rdto.ParentID)).Select(r => r.Id).FirstOrDefault();
            }

            context.Roles.Add(_role);
            await context.SaveChangesAsync();
            return _role;
        }
    }
}
