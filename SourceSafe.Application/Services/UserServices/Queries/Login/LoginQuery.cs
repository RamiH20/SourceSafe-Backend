using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.UserServices.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<LoginResult>>;

