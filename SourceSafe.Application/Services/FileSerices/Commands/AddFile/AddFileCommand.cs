using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.FileSerices.Commands.AddFile;

public record AddFileCommand(
    string Name,
    int GroupId,
    string Path,
    int UserId): IRequest<ErrorOr<AddFileResult>>;
