using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceSafe.Application.Common.Interfaces.Services;
using SourceSafe.Application.Services.FileSerices.Commands.AddFile;
using SourceSafe.Application.Services.FileSerices.Commands.Check_in;
using SourceSafe.Application.Services.FileSerices.Commands.Check_out;
using SourceSafe.Application.Services.FileSerices.Commands.DeleteFile;
using SourceSafe.Application.Services.FileSerices.Queries.GetGroupFiles;
using SourceSafe.Contracts.File;
using SourceSafe.Contracts.Group;

namespace SourceSafe.API.Controllers;
[Route("File")]
public class FileController(
    ISender mediator,
    IMapper mapper,
    IFileService fileService) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IFileService _fileService = fileService;
    [HttpPost]
    [Route("AddFile")]
    public async Task<IActionResult> AddFile([FromForm] AddFileRequest request)
    {
        string file = null!;
        if (request.FormFile != null)
        {
            var fileResult = _fileService.SaveFile(request.FormFile);
            file = fileResult;
        }
        var command = new AddFileCommand(
            request.Name,
            request.GroupId,
            file,
            request.UserId);
        var result = await _mediator.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<AddFileResponse>(result)),
            Problem);
    }
    [HttpPost]
    [Route("Check_in")]
    public async Task<IActionResult> Check_in(Check_inRequest request)
    {
        var command = _mapper.Map<Check_inCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<Check_inResponse>(result)),
            Problem);
    }
    [HttpPost]
    [Route("Check_out")]
    public async Task<IActionResult> Check_out(Check_outRequest request)
    {
        string file = null!;
        if (request.FormFile != null)
        {
            var fileResult = _fileService.SaveFile(request.FormFile);
            file = fileResult;
        }
        var command = new Check_outCommand(
            request.UserId,
            request.FileId,
            file,
            request.Edited);
        var result = await _mediator.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<Check_outResponse>(result)),
            Problem);
    }
    [HttpGet]
    [Route("GetGroupFiles")]
    public async Task<IActionResult> GetGroupFiles(int GroupId)
    {
        var result = await _mediator.Send(new GetGroupFilesQuery(GroupId));
        return result.Match(
            result => Ok(_mapper.Map<GetUserGroupsResponse>(result)),
            Problem);
    }
    [HttpPost]
    [Route("DeleteFile")]
    public async Task<IActionResult> DeleteFile(int FileId)
    {
        var result = await _mediator.Send(new DeleteFileCommand(FileId));
        return result.Match(
            result => Ok(_mapper.Map<Check_inResponse>(result)),
            Problem);
    }
}
