using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Commands.AddUserToGroup;

public record AddUserToGroupResult(
    HttpStatusCode Status,
    string Message);
