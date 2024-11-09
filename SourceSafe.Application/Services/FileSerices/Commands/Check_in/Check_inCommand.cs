using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_in;

public record Check_inCommand(
    int UserId,
    List<int> FileIds) : IRequest<ErrorOr<Check_inResult>>;
