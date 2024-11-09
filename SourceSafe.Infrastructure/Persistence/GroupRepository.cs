using Microsoft.EntityFrameworkCore;
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
}
