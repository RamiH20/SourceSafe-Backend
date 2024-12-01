namespace SourceSafe.Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public User Admin { get; set; } = null!;
}
