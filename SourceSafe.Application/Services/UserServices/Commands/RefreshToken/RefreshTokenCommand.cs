using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.UserServices.Commands.RefreshToken;

public record RefreshTokenCommand(
    string Token):IRequest<ErrorOr<RefreshTokenResult>>;
