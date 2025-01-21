using Microsoft.EntityFrameworkCore;
using SourceSafe.Application.Common.DTOs;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Entities;
using SourceSafe.Infrastructure.Data;

namespace SourceSafe.Infrastructure.Persistence;

public class ReportRepository(SourceSafeDbContext dbContext):
    IReportRepository
{
    private readonly SourceSafeDbContext _dbContext = dbContext;
    public async Task AddReport(Report report)
    {
        _dbContext.Reports.Add(report);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateReport(int fileId, int userId,
        DateTime Checked_outAt, bool Edited)
    {
        var report = await _dbContext.Reports
            .Where(x => x.File.Id == fileId && x.User.Id == userId)
            .OrderBy(x => x.Checked_inAt)
            .LastOrDefaultAsync();
        if (report != null)
        {
            report.Checked_outAt = Checked_outAt;
            report.Edited = Edited;
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<List<FileReportDTO>> GetFileReport(int fileId)
    {
        return await _dbContext.Reports.
            Where(x => x.File.Id == fileId).
            Select(x => new FileReportDTO
            {
                UserName = x.User.Name,
                Checked_inAt = x.Checked_inAt,
                Checked_outAt = x.Checked_outAt
            }).OrderByDescending(x => x.Checked_inAt).ToListAsync();
    }
    public async Task<List<UserReportDTO>> GetUsersReport(int groupId, int userId)
    {
        var reports = await _dbContext.Reports
            .Where(x => x.File.Group.Id == groupId && x.User.Id == userId)
            .Select(x => new
            {
                UserName = x.User.Name,
                FileName = x.File.Name,
                x.Checked_inAt,
                x.Checked_outAt
            })
            .ToListAsync();

        var groupedReports = reports
            .GroupBy(r => r.UserName)
            .Select(g => new UserReportDTO
            {
                UserName = g.Key,
                Files = g
                    .OrderByDescending(r => r.Checked_inAt)
                    .Select(r => new FilesUserReportDTO
                    {
                        FileName = r.FileName,
                        Checked_inAt = r.Checked_inAt,
                        Checked_outAt = r.Checked_outAt
                    })
                    .ToList()
            })
            .ToList();

        return groupedReports;
    }

}
