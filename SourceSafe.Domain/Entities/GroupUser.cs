namespace SourceSafe.Domain.Entities;

public class GroupUser
{
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public Group Group { get; set; } = null!;
}
