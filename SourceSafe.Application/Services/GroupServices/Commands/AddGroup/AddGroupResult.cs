using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Commands.AddGroup;

public record AddGroupResult(
    HttpStatusCode Status,
    string Message);
