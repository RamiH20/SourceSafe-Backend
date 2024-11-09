namespace SourceSafe.Contracts.File;

public class Check_inRequest
{
    public int UserId { get; set; }
    public List<int> FileIds { get; set; } = [];
}
