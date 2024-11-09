using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_out;

public record Check_outResult(
    HttpStatusCode Status,
    string Message);
