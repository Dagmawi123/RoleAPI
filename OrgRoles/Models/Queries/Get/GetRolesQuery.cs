using MediatR;

namespace OrgRoles.Models.Queries.Get
{
    public record GetRolesQuery() : IRequest<string>;
}
