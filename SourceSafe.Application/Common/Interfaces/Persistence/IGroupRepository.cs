using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Persistence;

public interface IGroupRepository
{
    Task<bool> DupblicateName(string name);
    Task AddGroup(Group group);
    Task AddGroupUsers(List<GroupUser> groupUsers);
}
