using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceSafe.Application.Services.GroupServices.Commands.AddGroup;
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
}
