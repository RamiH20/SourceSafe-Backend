using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Application.Common.Interfaces.Services;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_in;

public class Check_inCommandHandler(
    IFileRepository fileRepository,
    IUserRepository userRepository,
    IReportRepository reportRepository,
    IDateTimeProvider dateTimeProvider):
    IRequestHandler<Check_inCommand, ErrorOr<Check_inResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IReportRepository _reportRepository = reportRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<ErrorOr<Check_inResult>> Handle(Check_inCommand request, CancellationToken cancellationToken)
    {
        if (!_fileRepository.FilesAvailable(request.FileIds).Result)
        {
            return Errors.File.NotAvailable;
        }
        await _fileRepository.ReserveFiles(request.FileIds);
        foreach (var fileId in request.FileIds)
        {
            var file = await _fileRepository.GetFile(fileId);
            var user = await _userRepository.GetUserById(request.UserId);
            if (user is not null && file is not null)
            {
                var report = new Report()
                {
                    File = file,
                    User = user,
                    Checked_inAt = _dateTimeProvider.Now
                };
                await _reportRepository.AddReport(report);
            }
        }
        return new Check_inResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            "Checked_in successfully");
    }
}
