using Microsoft.EntityFrameworkCore;
using SourceSafe.Application.Common.DTOs;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Entities;
using SourceSafe.Infrastructure.Data;

namespace SourceSafe.Infrastructure.Persistence;

public class GroupRepository(SourceSafeDbContext dbContext) : IGroupRepository
{
    private readonly SourceSafeDbContext _dbContext = dbContext;
    public async Task<bool> DupblicateName(string name)
    {
        return await _dbContext.Groups.AnyAsync(x => x.Name == name);
    }
    public async Task AddGroup(Group group)
    {
        await _dbContext.Groups.AddAsync(group);
        await _dbContext.SaveChangesAsync();
    }
    public async Task AddGroupUsers(List<GroupUser> groupUsers)
    {
        await _dbContext.GroupUsers.AddRangeAsync(groupUsers);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<List<UserGroupDTO>> GetUserGroups(int userId)
    {
        List<UserGroupDTO> userGroups = [];
        var groups = await _dbContext.GroupUsers
            .Where(x => x.User.Id == userId)
            .Include(x => x.Group.Admin)
            .Select(x => x.Group)
            .ToListAsync();
        foreach(var group in groups)
        {
            var usersCount = await _dbContext.GroupUsers
                .Where(x => x.Group.Id == group.Id).CountAsync();
            var filesCount = await _dbContext.Files.
                Where(x => x.Group.Id == group.Id).CountAsync();
            userGroups.Add(new UserGroupDTO
            {
                GroupId = group.Id,
                GroupName = group.Name,
                GroupAdminName = group.Admin.Name,
                UsersCount = usersCount,
                FilesCount = filesCount
            });
        }
        return userGroups;
    }
    public async Task<List<User>> GetGroupUsers(int groupId)
    {
        return await _dbContext.GroupUsers
            .Where(x => x.Group.Id == groupId)
            .Select(x => x.User).ToListAsync();
    }
}
