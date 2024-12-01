using System.Net;

namespace SourceSafe.Contracts.Group;

public class GetUserGroupsResponse
{
    public HttpStatusCode Status { get; set; }
    public List<object> Items { get; set; } = [];
}
