using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetGroupUsers;

public record GetGroupUsersQuery(
    int GroupId): IRequest<ErrorOr<GetGroupUsersResult>>;
