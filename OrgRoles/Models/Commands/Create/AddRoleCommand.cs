using MediatR;

namespace OrgRoles.Models.Commands.Create
{
    public record AddRoleCommand(string Name, string Description, Guid? ParentID,bool isCandidate=false) : IRequest <Role> ;
}
