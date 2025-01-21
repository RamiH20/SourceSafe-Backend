using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.DTOs;

public class FileReportDTO
{
    public int UserId { get; set; }
    public DateTime Checked_inAt { get; set; }
    public DateTime? Checked_outAt { get; set; }
}
