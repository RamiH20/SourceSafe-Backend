using System.Net;

namespace SourceSafe.Contracts.File;

public class Check_inResponse
{
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; } = null!;
}
