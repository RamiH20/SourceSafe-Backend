using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetUserGroups;

public record GetUserGroupsQuery(
    int UserId): IRequest<ErrorOr<GetUserGroupsResult>>;
