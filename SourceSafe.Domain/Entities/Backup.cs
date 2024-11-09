namespace SourceSafe.Domain.Entities;

public class Backup
{
    public int Id { get; set; }
    public File File { get; set; } = null!;
    public string BackupPath { get; set; } = null!;
    public User User { get; set; } = null!;
    public DateTime Date { get; set; }
}
