using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.GroupServices.Queries.GetGroupUsers;

public class GetGroupUsersQueryHandler :
    IRequestHandler<GetGroupUsersQuery, ErrorOr<GetGroupUsersResult>>
{
    public Task<ErrorOr<GetGroupUsersResult>> Handle(GetGroupUsersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
