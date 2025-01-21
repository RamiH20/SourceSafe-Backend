namespace SourceSafe.Application.Common.DTOs;

public class FilesUserReportDTO
{
    public string FileName { get; set; } = null!;
    public DateTime Checked_inAt { get; set; }
    public DateTime? Checked_outAt { get; set; }
}
