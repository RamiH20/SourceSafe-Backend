using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Authentication;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Application.Common.Interfaces.Services;
using SourceSafe.Domain.Common.Errors;
using System.Net;

namespace SourceSafe.Application.Services.UserServices.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IDateTimeProvider dateTimeProvider):
    IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<ErrorOr<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByToken(request.Token).Result;
        if(user == null)
        {
            return Errors.User.NoUser;
        }
        var refreshToken = user.RefreshTokens
            .Single(t => t.Token == request.Token);
        if (!refreshToken.IsActive)
        {
            return Errors.User.InactiveToken;
        }
        refreshToken.RevokedOn = _dateTimeProvider.Now;
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        await _userRepository.AddRefreshToken(user, newRefreshToken);
        var token = await _jwtTokenGenerator.GenerateToken(user);
        return new RefreshTokenResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            token,
            newRefreshToken.Token,
            newRefreshToken.ExpiresOn);
    }
}
