﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;

namespace OrgRoles.Models.Commands.Delete
{
    public class RemoveRoleCommandHandler(IGetRepository getRepository,IRoleCommandsRepository commandsRepository) : IRequestHandler<RemoveRoleCommand> 
    {
       
        public async Task Handle(RemoveRoleCommand rrc,CancellationToken token) 
        { 
            List<Role> roles = await getRepository.GetRoles();
         RemoveRecursive(rrc.role,roles);
            await commandsRepository.SaveChanges();
            return;
        }

        public void RemoveRecursive(Role role,List<Role> roles)
        {

            List<Role> childRoles = roles
               .Where(r => r.ParentId == role.Id)
               .ToList();

            foreach (Role ChildRole in childRoles)
            {
                RemoveRecursive(ChildRole,roles);
            }
           commandsRepository.RemoveRole(role);
        }
    }
}
