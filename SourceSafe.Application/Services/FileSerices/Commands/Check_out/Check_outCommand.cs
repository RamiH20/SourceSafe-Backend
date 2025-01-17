using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_out;

public record Check_outCommand(
    int UserId,
    int FileId,
    string Path,
    bool Edited): IRequest<ErrorOr<Check_outResult>>;
