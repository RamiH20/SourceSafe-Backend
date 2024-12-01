namespace SourceSafe.Application.Common.DTOs;

public class UserGroupsDTO
{
    public string GroupName { get; set; } = null!;
    public string GroupAdminName { get; set; } = null!;
    public int UsersCount { get; set; }
    public int FilesCount { get; set; }
}
