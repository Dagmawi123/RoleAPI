
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;
using System.Data;

namespace OrgRoles.Models.Commands.Delete
{
    public class RemoveSingleRoleCommandHandler(IGetRepository getRepository,IRoleCommandsRepository commandsRepository) : IRequestHandler<RemoveSingleRoleCommand>
    {
        public async Task Handle(RemoveSingleRoleCommand rrc, CancellationToken token)
        {
            List<Role> roles = await getRepository.GetRoles();
            List<Role> childRoles = roles
                                      .Where(r => ( r.ParentId == rrc.role.Id))
                                      .ToList();
            foreach (Role childRole in childRoles)
            {
                childRole.ParentId = rrc.role.ParentId;
               await commandsRepository.UpdateRole(childRole);
                
            }
            commandsRepository.RemoveRole(rrc.role);
            //context.Roles.Remove(rrc.role);
            await commandsRepository.SaveChanges();
        }
    }
}
