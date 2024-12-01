using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Authentication;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

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
        var token = await _jwtTokenGenerator.GenerateToken(user);
        if ( user.RefreshTokens.Any(t => t.IsActive))
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
            return new LoginResult(
                (HttpStatusCode)StatusCodes.Status200OK,
                token,
                activeRefreshToken!.Token,
                activeRefreshToken.ExpiresOn);
        }
        else
        {
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            await _userRepository.AddRefreshToken(user, refreshToken);
            return new LoginResult(
                (HttpStatusCode)StatusCodes.Status200OK,
                token,
                refreshToken!.Token,
                refreshToken.ExpiresOn);
        }
    }
}
