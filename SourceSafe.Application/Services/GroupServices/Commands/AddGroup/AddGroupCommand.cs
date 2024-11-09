using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.GroupServices.Commands.AddGroup;

public record AddGroupCommand(
    int UserId,
    string GroupName,
    List<int> UserIds): IRequest<ErrorOr<AddGroupResult>>;
