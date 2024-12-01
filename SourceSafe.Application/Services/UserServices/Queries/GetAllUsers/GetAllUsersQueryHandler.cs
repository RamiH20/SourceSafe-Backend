using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using System.Net;

namespace SourceSafe.Application.Services.UserServices.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUsersQuery, ErrorOr<GetAllUsersResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<ErrorOr<GetAllUsersResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers(request.Id,
            request.Search);
        if (users == null)
        {
            return Errors.User.NoUser;
        }
        return new GetAllUsersResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            users);
    }
}
