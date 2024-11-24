using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.UserServices.Queries.GetAllUsers;

public record GetAllUsersQuery(
    int Id): IRequest<ErrorOr<GetAllUsersResult>>;
