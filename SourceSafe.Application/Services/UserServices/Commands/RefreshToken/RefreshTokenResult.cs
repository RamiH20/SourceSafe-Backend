using System.ComponentModel;
using System.Net;

namespace SourceSafe.Application.Services.UserServices.Commands.RefreshToken;

public record RefreshTokenResult(
    HttpStatusCode Status,
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpiration);
