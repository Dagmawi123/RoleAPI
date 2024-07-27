using MediatR;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Commands.Create;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;
using System.Data;

namespace OrgRoles.Models.Commands.Update
{
    public class UpdateRoleCommandHandler(IGetRepository getRepository,IRoleCommandsRepository commandsRepository) : IRequestHandler<UpdateRoleCommand, Role>
    {
        public async Task<Role> Handle(UpdateRoleCommand updateRolecommand, CancellationToken token)
        {
            Role? role = await getRepository.GetRole(updateRolecommand.id);
            if (role == null)
                return null;
            else
                role.Name = updateRolecommand.Name;
           role.Description = updateRolecommand.Description;

            if (updateRolecommand.ParentID!=null) 
            {
                if (await getRepository.CheckRole(updateRolecommand.ParentID.Value))
                    role.ParentId = updateRolecommand.ParentID;
            }
       await commandsRepository.UpdateRole(role);
              return role;
        }
    
    }

}
