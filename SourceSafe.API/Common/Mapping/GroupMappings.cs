using AutoMapper;
using SourceSafe.Application.Services.GroupServices.Commands.AddGroup;
using SourceSafe.Contracts.Group;

namespace SourceSafe.API.Common.Mapping;

public class GroupMappings : Profile
{
    public GroupMappings()
    {
        CreateMap<AddGroupRequest, AddGroupCommand>();
        CreateMap<AddGroupResult, AddGroupResponse>();
    }
}
