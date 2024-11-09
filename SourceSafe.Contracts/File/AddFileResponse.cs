using System.Net;

namespace SourceSafe.Contracts.File;

public class AddFileResponse
{
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; } = null!;
}
