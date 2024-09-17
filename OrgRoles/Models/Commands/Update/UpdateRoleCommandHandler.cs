using MediatR;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Commands.Create;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;
using System.Data;

namespace OrgRoles.Models.Commands.Update
{
    public record UpdateRoleCommand(Guid id, string Name, string Description, Guid? ParentID) : IRequest<Role>;

    public class UpdateRoleCommandHandler(IRoleCommandsRepository commandsRepository) : IRequestHandler<UpdateRoleCommand, Role>
    {
        public async Task<Role> Handle(UpdateRoleCommand updateRolecommand, CancellationToken token)
        {
            Role? role = await commandsRepository.GetRole(updateRolecommand.id);
            if (role == null)
                return null;
            else
                role.Name = updateRolecommand.Name;
           role.Description = updateRolecommand.Description;

            if (updateRolecommand.ParentID!=null) 
            {
                if (await commandsRepository.CheckRole(updateRolecommand.ParentID.Value))
                    role.ParentId = updateRolecommand.ParentID;
            }
       await commandsRepository.UpdateRole(role);
              return role;
        }
    
    }

}
