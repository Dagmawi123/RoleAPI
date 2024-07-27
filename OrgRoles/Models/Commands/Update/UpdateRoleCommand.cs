using MediatR;

namespace OrgRoles.Models.Commands.Update
{
    public record UpdateRoleCommand(Guid id, string Name, string Description, Guid? ParentID) : IRequest<Role>;
}
