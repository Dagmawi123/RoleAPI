
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;
using System.Data;
using System.Data.Common;

namespace OrgRoles.Models.Commands.Delete
{
    public record RemoveSingleRoleCommand(Role role) : IRequest;
    public class RemoveSingleRoleCommandHandler(IRoleCommandsRepository commandsRepository) : IRequestHandler<RemoveSingleRoleCommand>
    {
        public async Task Handle(RemoveSingleRoleCommand rrc, CancellationToken token)
        {
            ////transaction begin
            //try {

                
            //    //transaction commit
            //}
            //catch () {
            //    //transaction rollback
            //}
            //finally { 
            ////transaction release
            //}
            
          await PromoteChildren(rrc.role);
            await commandsRepository.SaveChanges();
          //  List<Role> children = await getRepository.GetSuccessors(rrc.role.Id);
            commandsRepository.RemoveRole(rrc.role);
            await commandsRepository.SaveChanges();
            //context.Roles.Remove(rrc.role);

        }
        public async Task PromoteChildren(Role role)
        {
            List<Role> children = await commandsRepository.GetImmediateChildren(role.Id);
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
