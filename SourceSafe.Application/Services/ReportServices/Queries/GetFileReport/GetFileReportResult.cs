using SourceSafe.Application.Common.DTOs;
using System.Net;

namespace SourceSafe.Application.Services.ReportServices.Queries.GetFileReport;

public record GetFileReportResult(
    HttpStatusCode Status,
    List<FileReportDTO> Items);
