using SourceSafe.Application.Common.DTOs;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Queries.GetGroupFiles;

public record GetGroupFilesResult(
    HttpStatusCode Status,
    List<GroupFileDTO> Items);
