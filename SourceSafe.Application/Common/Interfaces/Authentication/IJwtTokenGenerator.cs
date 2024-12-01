using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(User user);
    RefreshToken GenerateRefreshToken();
}
