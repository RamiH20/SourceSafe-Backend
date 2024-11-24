using SourceSafe.Application.Common.DTOs;
using System.Net;

namespace SourceSafe.Application.Services.UserServices.Queries.GetAllUsers;

public record GetAllUsersResult(
    HttpStatusCode Status,
    List<UserDTO> Users);
