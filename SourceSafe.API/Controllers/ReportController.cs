using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceSafe.Application.Services.ReportServices.Queries.GetFileReport;
using SourceSafe.Application.Services.ReportServices.Queries.GetUsersReport;
using SourceSafe.Contracts.File;
using SourceSafe.Contracts.Group;

namespace SourceSafe.API.Controllers;
[Route("Report")]
public class ReportController(
    ISender mediator,
    IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    [HttpGet]
    [Route("GetFileReport")]
    public async Task<IActionResult> GetFileReport(int FileId)
    {
        var result = await _mediator.Send(new GetFileReportQuery(FileId));
        return result.Match(
            result => Ok(_mapper.Map<GetUserGroupsResponse>(result)),
            Problem);
    }
    [HttpGet]
    [Route("GetUserReport")]
    public async Task<IActionResult> GetUserReport(int GroupId)
    {
        var result = await _mediator.Send(new GetUsersReportQuery(GroupId));
        return result.Match(
            result => Ok(_mapper.Map<GetUserGroupsResponse>(result)),
            Problem);
    }
}
