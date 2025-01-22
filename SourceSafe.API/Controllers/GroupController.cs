using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceSafe.Application.Services.GroupServices.Commands.AddGroup;
using SourceSafe.Application.Services.GroupServices.Commands.AddUserToGroup;
using SourceSafe.Application.Services.GroupServices.Queries.GetUserGroups;
using SourceSafe.Contracts.Group;

namespace SourceSafe.API.Controllers;
[Route("Group")]
public class GroupController(
    ISender mediator,
    IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Route("AddGroup/{UserId}")]
    public async Task<IActionResult> AddGroup(int UserId, [FromForm]AddGroupRequest request)
    {
        var command = new AddGroupCommand(UserId,request.GroupName,request.UserIds);
        var result = await _mediator.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<AddGroupResponse>(result)),
            Problem);
    }
    [HttpGet]
    [Route("GetUserGroups/{UserId}")]
    public async Task<IActionResult> GetUserGroups(int UserId)
    {
        var result = await _mediator.Send(new GetUserGroupsQuery(UserId));
        return result.Match(
            result => Ok(_mapper.Map<GetUserGroupsResponse>(result)),
            Problem);
    }
    [HttpPost]
    [Route("AddUserToGroup")]
    public async Task<IActionResult> AddUserToGroup([FromQuery] AddUserToGroupRequest request)
    {
        var command = _mapper.Map<AddUserToGroupCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<AddGroupResponse>(result)),
            Problem);
    }
}
