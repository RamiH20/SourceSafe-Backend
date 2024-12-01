namespace SourceSafe.Application.Common.DTOs;

public class GroupMembersDTO
{
    public int UserName { get; set; }
    public Dictionary<string, List<string>> FileCopies { get; set; } = []; 
    public DateTime Date { get; set; }
}
