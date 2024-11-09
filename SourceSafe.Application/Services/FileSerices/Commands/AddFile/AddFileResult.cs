using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.AddFile;

public record AddFileResult(
    HttpStatusCode Status,
    string Message);
