using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceSafe.Application.Services.UserServices.Commands.Register;
using SourceSafe.Application.Services.UserServices.Queries.GetAllUsers;
using SourceSafe.Application.Services.UserServices.Queries.Login;
using SourceSafe.Contracts.User;

namespace SourceSafe.API.Controllers;

[Route("User")]
public class UserController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<RegisterResponse>(result)),
            Problem);
    }
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<LoginResponse>(result)),
            Problem);
    }
    [HttpGet]
    [Route("GetAllUsers/{Id}")]
    public async Task<IActionResult> GetAllUsers(int Id)
    {
        var result = await _mediator.Send(new GetAllUsersQuery(Id));
        return result.Match(
            result => Ok(_mapper.Map<GetAllUsersResponse>(result)),
            Problem);
    }
}
