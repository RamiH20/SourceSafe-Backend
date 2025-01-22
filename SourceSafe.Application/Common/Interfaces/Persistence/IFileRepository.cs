using SourceSafe.Application.Common.DTOs;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Persistence;

public interface IFileRepository
{
    Task Add(Domain.Entities.File file);
    Task<bool> DuplicateName(string name);
    Task<Group?> GetGroup(int id);
    Task<Domain.Entities.File?> GetFile(int id);
    Task<bool> FilesAvailable(List<int> fileIds);
    Task ReserveFiles(List<int> fileIds);
    Task AddBackup(Backup backup);
    Task Check_outFile(int fileId);
    Task ReplaceFilePath(int fileId, string newPath);
    Task<List<GroupFileDTO>> GetGroupFiles(int groupId);
    Task DeleteFile(Domain.Entities.File file);
    Task<List<FileCopiesDTO>> GetFileCopies(int fileId);
}
