using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.GroupServices.Commands.AddUserToGroup;

public record AddUserToGroupCommand(
    int GroupId,
    int UserId) : IRequest<ErrorOr<AddUserToGroupResult>>;
