using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Commands.AddGroup;

public class AddGroupCommandHandler(
    IGroupRepository groupRepository,
    IUserRepository userRepository):
    IRequestHandler<AddGroupCommand, ErrorOr<AddGroupResult>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<ErrorOr<AddGroupResult>> Handle(AddGroupCommand request, CancellationToken cancellationToken)
    {
        request.UserIds.Add(request.UserId);
        if (_groupRepository.DupblicateName(request.GroupName).Result)
        {
            return Errors.File.DuplicateName;
        }
        var admin = await _userRepository.GetUserById(request.UserId);
        var group = new Group()
        {
            Name = request.GroupName,
            Admin = admin
        };
        await _groupRepository.AddGroup(group);
        List<User> users = [];
        List<GroupUser> groupUsers = [];
        foreach(var userId in request.UserIds)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user != null)
            {
                users.Add(user);
            }
        }
        foreach (var user in users)
        {
            var groupUser = new GroupUser()
            {
                User = user,
                Group = group
            };
            groupUsers.Add(groupUser);
        }
        await _groupRepository.AddGroupUsers(groupUsers);
        return new AddGroupResult(
            (HttpStatusCode)StatusCodes.Status201Created,
            "Group created successfully");
    }
}
