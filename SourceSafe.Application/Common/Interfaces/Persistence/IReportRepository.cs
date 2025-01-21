using SourceSafe.Application.Common.DTOs;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Persistence;

public interface IReportRepository
{
    Task AddReport(Report report);
    Task UpdateReport(int fileId, int userId, 
        DateTime Checked_outAt, bool Edited);
    Task<List<FileReportDTO>> GetFileReport(int fileId);
    Task<List<UserReportDTO>> GetUsersReport(int groupId, int userId);
}
