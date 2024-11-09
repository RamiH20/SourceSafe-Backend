using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_in;

public record Check_inResult(
    HttpStatusCode Status,
    string Message);
