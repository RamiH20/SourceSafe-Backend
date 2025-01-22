using SourceSafe.Application.Common.DTOs;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Persistence;

public interface IGroupRepository
{
    Task<bool> DupblicateName(string name);
    Task AddGroup(Group group);
    Task AddGroupUsers(List<GroupUser> groupUsers);
    Task<List<UserGroupDTO>> GetUserGroups(int UserId);
    Task<List<User>> GetGroupUsers(int groupId);
    Task<Group?> GetGroupById(int groupId);
    Task AddGroupUser(GroupUser groupUser);
}
