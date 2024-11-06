using ErrorOr;
using MediatR;
using SourceSafe.Application.Common.Interfaces.Authentication;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Services.UserServices.Queries.Login;

public class LoginQueryHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly IUserRepository _userRepository = userRepository; 
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator; 
    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.User.InvalidCredentials;
        }
        // Validate the password
        if (!_userRepository.CheckPassword(query.Email, query.Password).Result)
        {
            return Errors.User.InvalidCredentials;
        }
        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new LoginResult(token);
    }
}
