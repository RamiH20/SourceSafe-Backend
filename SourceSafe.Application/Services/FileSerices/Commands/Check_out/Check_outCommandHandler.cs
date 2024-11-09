using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Application.Common.Interfaces.Services;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.Check_out;

public class Check_outCommandHandler(
    IFileRepository fileRepository,
    IUserRepository userRepository,
    IDateTimeProvider dateTimeProvider):
    IRequestHandler<Check_outCommand, ErrorOr<Check_outResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<ErrorOr<Check_outResult>> Handle(Check_outCommand request, CancellationToken cancellationToken)
    {
        await _fileRepository.Check_outFile(request.FileId);
        await _fileRepository.ReplaceFilePath(request.FileId, request.Path);
        var file = _fileRepository.GetFile(request.FileId).Result;
        if(file is null)
        {
            return Errors.File.FileNotFound;
        }
        var user = _userRepository.GetUserById(request.UserId).Result;
        if(user is null)
        {
            return Errors.User.NoUser;
        }
        var backup = new Backup()
        {
            File = file,
            BackupPath = request.Path,
            User = user,
            Date = _dateTimeProvider.Now
        };
        await _fileRepository.AddBackup(backup);
        return new Check_outResult(
            (HttpStatusCode)StatusCodes.Status200OK,
            "Checked_out successfully");
    }
}
