using ErrorOr;
using MediatR;

namespace SourceSafe.Application.Services.ReportServices.Queries.GetUsersReport;

public record GetUsersReportQuery(
    int GroupId):IRequest<ErrorOr<GetUsersReportResult>>;
