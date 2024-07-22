using MediatR;

namespace OrgRoles.Models.Queries.Get
{
    public record GetRoleQuery(Guid Id) : IRequest<Role>;
}
