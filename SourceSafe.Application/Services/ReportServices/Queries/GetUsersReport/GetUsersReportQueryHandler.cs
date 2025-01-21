using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.DTOs;
using SourceSafe.Application.Common.Interfaces.Persistence;
using System.Net;

namespace SourceSafe.Application.Services.ReportServices.Queries.GetUsersReport;

public class GetUsersReportQueryHandler(
    IReportRepository reportRepository,
    IGroupRepository groupRepository) :
    IRequestHandler<GetUsersReportQuery, ErrorOr<GetUsersReportResult>>
{
    private readonly IReportRepository _reportRepository = reportRepository;
    private readonly IGroupRepository _groupRepository = groupRepository;
    public async Task<ErrorOr<GetUsersReportResult>> Handle(GetUsersReportQuery request, CancellationToken cancellationToken)
    {
        var users = await _groupRepository.GetGroupUsers(request.GroupId);
        List<UserReportDTO> report = [];
        foreach (var user in users)
        {
            var userReport = await _reportRepository.GetUsersReport(request.GroupId, user.Id);
            report.AddRange(userReport);
        }
        return new GetUsersReportResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            report);
    }
}
