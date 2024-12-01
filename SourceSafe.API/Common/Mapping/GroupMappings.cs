using AutoMapper;
using SourceSafe.Application.Services.GroupServices.Commands.AddGroup;
using SourceSafe.Application.Services.GroupServices.Queries.GetUserGroups;
using SourceSafe.Application.Services.UserServices.Queries.GetAllUsers;
using SourceSafe.Contracts.Group;
using SourceSafe.Contracts.User;

namespace SourceSafe.API.Common.Mapping;

public class GroupMappings : Profile
{
    public GroupMappings()
    {
        CreateMap<AddGroupRequest, AddGroupCommand>();
        CreateMap<AddGroupResult, AddGroupResponse>();
        CreateMap<GetUserGroupsResult, GetUserGroupsResponse>()
            .ForMember(dest => dest.Items,
            opt => opt.MapFrom(src => src.Items));
    }
}
