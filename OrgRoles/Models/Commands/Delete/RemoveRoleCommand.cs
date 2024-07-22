using MediatR;

namespace OrgRoles.Models.Commands.Delete
{
    public record RemoveRoleCommand(Role role) : IRequest;
}
