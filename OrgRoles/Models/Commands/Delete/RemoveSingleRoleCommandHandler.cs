
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
    
          await PromoteChildren(rrc.role);
            await commandsRepository.SaveChanges();
            List<Role> children = await getRepository.GetSuccessors(rrc.role.Id);
            commandsRepository.RemoveRole(rrc.role);
            await commandsRepository.SaveChanges();
            //context.Roles.Remove(rrc.role);

        }
        public async Task PromoteChildren(Role role)
        {
            List<Role> children = await getRepository.GetSuccessors(role.Id);
            if (children.Count() != 0) 
            {
               Role candidate= findCandidate(children);
                foreach (Role childRole in children)
                {
                    if (childRole.Id != candidate.Id) { 
                    childRole.ParentId= candidate.Id;
                        commandsRepository.NotifyChange(childRole);
                    }                        

                }               

                await PromoteChildren(candidate);
                if (role.ParentId == null) { 
                candidate.ParentId= role.ParentId;
                    commandsRepository.NotifyChange(candidate);
                }
                
            }
        }


        public Role findCandidate(List<Role> roles) {
            foreach (Role role in roles) 
            { 
            if(role.isCandidate)
                    return role;
            }
            return roles[0];
        }




    }
}
