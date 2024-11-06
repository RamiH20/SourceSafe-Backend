using System.Net;

namespace SourceSafe.Application.Services.UserServices.Commands.Register;

public record RegisterResult(
    HttpStatusCode Status,
    string Message);
