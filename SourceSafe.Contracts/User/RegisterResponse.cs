using System.Net;

namespace SourceSafe.Contracts.User;

public class RegisterResponse
{
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; } = null!;
}
