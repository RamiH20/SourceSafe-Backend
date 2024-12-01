using System.Net;

namespace SourceSafe.Application.Services.UserServices.Queries.Login;

public record LoginResult(
    HttpStatusCode Status,
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpiration);
