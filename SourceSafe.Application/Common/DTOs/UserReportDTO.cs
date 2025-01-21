namespace SourceSafe.Application.Common.DTOs;

public class UserReportDTO
{
    public string UserName { get; set; } = null!;
    public List<FilesUserReportDTO> Files { get; set; } = [];
}
