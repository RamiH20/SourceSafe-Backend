namespace SourceSafe.Application.Common.DTOs;

public class FilesUserReportDTO
{
    public int FileId { get; set; }
    public DateTime Checked_inAt { get; set; }
    public DateTime? Checked_outAt { get; set; }
}
