using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetUserGroups;

public class GetUserGroupsQueryHandler(IGroupRepository groupRepository):
    IRequestHandler<GetUserGroupsQuery, ErrorOr<GetUserGroupsResult>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    public async Task<ErrorOr<GetUserGroupsResult>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
    { 
        return new GetUserGroupsResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            await _groupRepository.GetUserGroups(request.UserId));
    }
}
