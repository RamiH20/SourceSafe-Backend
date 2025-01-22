using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

namespace SourceSafe.Application.Services.GroupServices.Commands.AddUserToGroup;

public class AddUserToGroupCommandHandler(
    IUserRepository userRepository,
    IGroupRepository groupRepository)
    : IRequestHandler<AddUserToGroupCommand, ErrorOr<AddUserToGroupResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IGroupRepository _groupRepository = groupRepository;
    public async Task<ErrorOr<AddUserToGroupResult>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        var group =  await _groupRepository.GetGroupById(request.GroupId);
        if (user == null)
        {
            return Errors.User.NoUser;
        }
        if (group == null)
        {
            return Errors.Group.NoGroup;
        }
        var groupUser = new GroupUser()
        {
            Group = group,
            User = user
        };
        await _groupRepository.AddGroupUser(groupUser);
        return new AddUserToGroupResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            $"User {user.Name} has been added to the group");
    }
}
