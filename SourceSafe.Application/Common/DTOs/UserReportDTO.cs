namespace SourceSafe.Application.Common.DTOs;

public class UserReportDTO
{
    public int UserId { get; set; }
    public List<FilesUserReportDTO> Files { get; set; } = [];
}
