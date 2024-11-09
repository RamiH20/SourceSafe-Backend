using Microsoft.EntityFrameworkCore;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Entities;
using SourceSafe.Infrastructure.Data;

namespace SourceSafe.Infrastructure.Persistence;

public class FileRepository(SourceSafeDbContext dbContext) : IFileRepository
{
    private readonly SourceSafeDbContext _dbContext = dbContext;
    public async Task Add(Domain.Entities.File file)
    {
        await _dbContext.Files.AddAsync(file);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<bool> DuplicateName(string name)
    {
        return await _dbContext.Files.AnyAsync(x => x.Name == name);
    }
    public async Task<Group?> GetGroup(int id)
    {
        return await _dbContext.Groups.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Domain.Entities.File?> GetFile(int id)
    {
        return await _dbContext.Files.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<bool> FilesAvailable(List<int> fileIds)
    {
        bool reserved = false;
        foreach (var id in fileIds)
        {
            var fileAvailability = await _dbContext.Files
                .Where(x => x.Id == id)
                .Select(x => x.Reserved)
                .FirstOrDefaultAsync();
            if (fileAvailability)
            {
                reserved = true;
            }
        }
        if (reserved)
        {
            return false;
        }
        return true;
    }
    public async Task ReserveFiles(List<int> fileIds)
    {
        foreach (var id in fileIds)
        {
            var file = await _dbContext.Files.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if(file is not null)
            {
                file.Reserved = true;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
    public async Task AddBackup(Backup backup)
    {
        await _dbContext.Backups.AddAsync(backup);
        await _dbContext.SaveChangesAsync();
    }
    public async Task Check_outFile(int fileId)
    {
        var file = await _dbContext.Files
            .FirstOrDefaultAsync(x => x.Id == fileId);
        if(file is not null)
        {
            file.Reserved = false;
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task ReplaceFilePath(int fileId, string newPath)
    {
        var file = await _dbContext.Files
           .FirstOrDefaultAsync(x => x.Id == fileId);
        if(file is not null)
        {
            file.Path = newPath;
            await _dbContext.SaveChangesAsync();
        }
    }
}
