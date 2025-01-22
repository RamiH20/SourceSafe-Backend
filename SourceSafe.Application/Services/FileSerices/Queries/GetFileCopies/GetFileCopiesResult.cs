using SourceSafe.Application.Common.DTOs;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Queries.GetFileCopies;

public record GetFileCopiesResult(
    HttpStatusCode Status,
    List<FileCopiesDTO> Items);
