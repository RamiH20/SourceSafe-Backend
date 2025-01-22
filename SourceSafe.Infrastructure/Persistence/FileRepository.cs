using Microsoft.EntityFrameworkCore;
using SourceSafe.Application.Common.DTOs;
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
    public async Task<List<GroupFileDTO>> GetGroupFiles(int groupId)
    {
        List<GroupFileDTO> files = [];
        List<int> fileIds = await _dbContext.Files
            .Where(x => x.Group.Id == groupId)
            .Select(x => x.Id)
            .ToListAsync();
        foreach (int fileId in fileIds)
        { 
            var file = await _dbContext.Files
                .FirstOrDefaultAsync(x => x.Id == fileId);
            var lastUpdated = await _dbContext.Backups
                    .Include(x => x.File)
                    .Where(x => x.File.Id == fileId)
                    .Select(x => x.Date)
                    .OrderBy(x => x.Date)
                    .LastOrDefaultAsync();
            var groupFileDTO = new GroupFileDTO
            {
                FileId = fileId,
                FileName = file!.Name,
                FilePath = file.Path,
                LastUpdated = lastUpdated,
                IsReserved = file.Reserved
            };
            files.Add(groupFileDTO);
        }
        return files;
    }
    public async Task DeleteFile(Domain.Entities.File file)
    {
        _dbContext.Files.Remove(file);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<List<FileCopiesDTO>> GetFileCopies(int fileId)
    {
        return await _dbContext.Backups
            .Where(x => x.File.Id == fileId)
            .Select(x => new FileCopiesDTO
            {
                CopyPath = x.BackupPath,
                Date = x.Date,
            }).OrderByDescending(x => x.Date).ToListAsync();
    }
}
