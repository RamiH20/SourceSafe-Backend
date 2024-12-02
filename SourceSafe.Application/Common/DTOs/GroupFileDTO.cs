namespace SourceSafe.Application.Common.DTOs;

public class GroupFileDTO
{
    public int FileId { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public DateTime LastUpdated { get; set; }
    public bool IsReserved { get; set; }
}
