using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.FileSerices.Commands.DeleteFile;

public record DeleteFileCommand(
    int FileId): IRequest<ErrorOr<DeleteFileResult>>;
