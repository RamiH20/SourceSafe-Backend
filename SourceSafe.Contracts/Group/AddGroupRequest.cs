namespace SourceSafe.Contracts.Group;

public class AddGroupRequest
{
    public string GroupName { get; set; } = null!;
    public List<int> UserIds { get; set; } = [];
}
