using MediatR;

namespace OrgRoles.Models.Commands.Update
{
    public record UpdateRoleCommand(Role role, RoleDTO rdto) : IRequest<Role>;
}
