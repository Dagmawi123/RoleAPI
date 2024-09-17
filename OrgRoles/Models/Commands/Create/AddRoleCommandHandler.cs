using MediatR;
using Microsoft.EntityFrameworkCore;
using OrgRoles.Models.Queries.Get;
using OrgRoles.Models.Repos;

namespace OrgRoles.Models.Commands.Create
{
        public record AddRoleCommand(string Name, string Description, Guid? ParentID, bool isCandidate = false) : IRequest<Role>;

       public class AddRoleCommandHandler(IRoleCommandsRepository roleCommandsRepo) :IRequestHandler<AddRoleCommand,Role>
        {
        public async Task<Role> Handle(AddRoleCommand ALC , CancellationToken token) {

            Role _role = new()
            {
                Name = ALC.Name,
                Description = ALC.Description,
                isCandidate=ALC.isCandidate
            };
            if (ALC.ParentID!=null)
            {
                if (await roleCommandsRepo.CheckRole(ALC.ParentID.Value))
                    _role.ParentId = ALC.ParentID;
            }
        _role= await roleCommandsRepo.AddRole(_role);
           return _role;
        }
    }

}
