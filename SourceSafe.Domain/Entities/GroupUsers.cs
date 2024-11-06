namespace SourceSafe.Domain.Entities;

public class GroupUsers
{
    public User User { get; set; } = null!;
    public Group Group { get; set; } = null!;
}
