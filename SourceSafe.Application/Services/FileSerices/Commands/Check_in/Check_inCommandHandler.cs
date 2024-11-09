using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_in;

public class Check_inCommandHandler(IFileRepository fileRepository):
    IRequestHandler<Check_inCommand, ErrorOr<Check_inResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    public async Task<ErrorOr<Check_inResult>> Handle(Check_inCommand request, CancellationToken cancellationToken)
    {
        if (!_fileRepository.FilesAvailable(request.FileIds).Result)
        {
            return Errors.File.NotAvailable;
        }
        await _fileRepository.ReserveFiles(request.FileIds);
        return new Check_inResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            "Checked_in successfully");
    }
}
