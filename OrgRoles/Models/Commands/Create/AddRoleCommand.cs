using MediatR;

namespace OrgRoles.Models.Commands.Create
{
    public record AddRoleCommand(RoleDTO rdto) : IRequest <Role> ;
}
