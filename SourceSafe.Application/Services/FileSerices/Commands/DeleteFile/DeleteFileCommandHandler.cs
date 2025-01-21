using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.DeleteFile;

public class DeleteFileCommandHandler(IFileRepository fileRepository):
    IRequestHandler<DeleteFileCommand, ErrorOr<DeleteFileResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    public async Task<ErrorOr<DeleteFileResult>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _fileRepository.GetFile(request.FileId);
        if (file == null)
        {
            return Errors.File.FileNotFound;
        }
        await _fileRepository.DeleteFile(file);
        return new DeleteFileResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            $"File {file.Name} Deleted successfully");
    }
}
