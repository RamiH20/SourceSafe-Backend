using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using System.Net;

namespace SourceSafe.Application.Services.ReportServices.Queries.GetFileReport;

public class GetFileReportQueryHandler(IReportRepository reportRepository):
    IRequestHandler<GetFileReportQuery, ErrorOr<GetFileReportResult>>
{
    private readonly IReportRepository _reportRepository = reportRepository;
    public async Task<ErrorOr<GetFileReportResult>> Handle(GetFileReportQuery request, CancellationToken cancellationToken)
    {
        return new GetFileReportResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            await _reportRepository.GetFileReport(request.FileId));
    }
}
