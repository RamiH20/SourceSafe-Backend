using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.UserServices.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Password) : IRequest<ErrorOr<RegisterResult>>;
