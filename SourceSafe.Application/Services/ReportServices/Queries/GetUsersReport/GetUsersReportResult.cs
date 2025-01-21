using SourceSafe.Application.Common.DTOs;
using System.Net;

namespace SourceSafe.Application.Services.ReportServices.Queries.GetUsersReport;

public record GetUsersReportResult(
    HttpStatusCode Status,
    List<UserReportDTO> Items);
