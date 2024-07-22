using MediatR;

namespace OrgRoles.Models.Commands.Delete
{
    public record RemoveSingleRoleCommand(Role role) : IRequest ;
}
