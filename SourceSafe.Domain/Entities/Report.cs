namespace SourceSafe.Domain.Entities;

public class Report
{
    public int Id { get; set; }
    public File File { get; set; } = null!;
    public User User { get; set; } = null!;
    public DateTime Checked_inAt { get; set; }
    public DateTime? Checked_outAt { get; set; }
    public bool Edited { get; set; } = false;
}
