using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Queries.GetFileCopies;

public class GetFileCopiesQueryHandler(IFileRepository fileRepository)
    : IRequestHandler<GetFileCopiesQuery, ErrorOr<GetFileCopiesResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    public async Task<ErrorOr<GetFileCopiesResult>> Handle(GetFileCopiesQuery request, CancellationToken cancellationToken)
    {
        return new GetFileCopiesResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            await _fileRepository.GetFileCopies(request.FileId));
    }
}
