using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

namespace SourceSafe.Application.Services.UserServices.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if(_userRepository.GetUserByEmail(request.Email) is not null) 
        {
            return Errors.User.DuplicateEmail;
        }
        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        };
        await _userRepository.Add(user);
        return new RegisterResult(
            (HttpStatusCode) StatusCodes.Status201Created,
            "Registered");
    }
}
