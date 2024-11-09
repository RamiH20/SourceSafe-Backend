using System.Net;

namespace SourceSafe.Contracts.Group;

public class AddGroupResponse
{
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; } = null!;
}
