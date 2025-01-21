using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.DeleteFile;

public record DeleteFileResult(
    HttpStatusCode Status,
    string Message);
