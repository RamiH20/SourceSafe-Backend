using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetGroupUsers;

public record GetGroupUsersResult(
    HttpStatusCode Status,
    List<GroupUsersDTO> Items);
