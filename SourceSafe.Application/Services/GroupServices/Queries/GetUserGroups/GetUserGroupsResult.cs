using SourceSafe.Application.Common.DTOs;
using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetUserGroups;

public record GetUserGroupsResult(
    HttpStatusCode Status,
    List<UserGroupDTO> Items);
