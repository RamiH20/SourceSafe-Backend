using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.FileSerices.Queries.GetFileCopies;

public record GetFileCopiesQuery(
    int FileId): IRequest<ErrorOr<GetFileCopiesResult>>;
