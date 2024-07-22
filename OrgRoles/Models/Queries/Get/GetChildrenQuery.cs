using MediatR;

namespace OrgRoles.Models.Queries.Get
{
    public record GetChildrenQuery (Guid Id,List<Role> children,List<Role> _roles):IRequest;
}
