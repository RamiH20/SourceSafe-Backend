namespace SourceSafe.Domain.Entities;

public class File
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public Group Group { get; set; } = null!;
    public bool State {  get; set; }
}
