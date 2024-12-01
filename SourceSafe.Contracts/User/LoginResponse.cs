using System.Net;
using System.Text.Json.Serialization;

namespace SourceSafe.Contracts.User;

public class LoginResponse
{
    public HttpStatusCode Status { get; set; }
    public string Token { get; set; } = null!;
    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiration { get; set; }
}
