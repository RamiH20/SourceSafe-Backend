using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetUserGroups;

public class GetUserGroupsQueryHandler(IGroupRepository groupRepository):
    IRequestHandler<GetUserGroupsQuery, ErrorOr<GetUserGroupsResult>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    public async Task<ErrorOr<GetUserGroupsResult>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetUserGroups(request.UserId);
        if ( groups.Count == 0)
        {
            return Errors.Group.UserWithNoGroup;
        }
        return new GetUserGroupsResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            groups);
    }
}
