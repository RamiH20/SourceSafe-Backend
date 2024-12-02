using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.FileSerices.Queries.GetGroupFiles;

public record GetGroupFilesQuery(
    int GroupId) : IRequest<ErrorOr<GetGroupFilesResult>>;
