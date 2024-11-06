using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
