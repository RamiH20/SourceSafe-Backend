using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Common.Errors;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Queries.GetGroupFiles;

public class GetGroupFilesQueryHandler(
    IFileRepository fileRepository) :
    IRequestHandler<GetGroupFilesQuery, ErrorOr<GetGroupFilesResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    public async Task<ErrorOr<GetGroupFilesResult>> Handle(GetGroupFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _fileRepository.GetGroupFiles(request.GroupId);
        if (files.Count == 0)
        {
            return Errors.File.GroupWithNoFiles;
        }
        return new GetGroupFilesResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            files);
    }
}
