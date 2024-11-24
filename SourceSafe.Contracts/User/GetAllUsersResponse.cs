using System.Net;

namespace SourceSafe.Contracts.User;

public class GetAllUsersResponse
{
    public HttpStatusCode Status { get; set; }
    public List<object> Items { get; set; } = [];
}
