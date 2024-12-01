using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SourceSafe.Application.Services.UserServices.Commands.RefreshToken;
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
        if(!string.IsNullOrEmpty(result.Value.RefreshToken))
        {
            SetRefreshTokenInCookie(result.Value.RefreshToken,result.Value.RefreshTokenExpiration);
        }
        return result.Match(
            result => Ok(_mapper.Map<LoginResponse>(result)),
            Problem);
    }
    [HttpGet]
    [Route("GetAllUsers")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersRequest request)
    {
        var query = _mapper.Map<GetAllUsersQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<GetAllUsersResponse>(result)),
            Problem);

    }
    [HttpGet]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if(string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest();
        }
        var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));
        SetRefreshTokenInCookie(result.Value.RefreshToken, result.Value.RefreshTokenExpiration);    
        return result.Match(
            result => Ok(_mapper.Map<LoginResponse>(result)),
            Problem);
    }
    private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime()
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
