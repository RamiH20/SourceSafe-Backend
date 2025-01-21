using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.ReportServices.Queries.GetFileReport;

public record GetFileReportQuery(
    int FileId):IRequest<ErrorOr<GetFileReportResult>>;
