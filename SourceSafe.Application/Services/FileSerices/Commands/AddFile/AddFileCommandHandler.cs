using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Application.Common.Interfaces.Services;
using SourceSafe.Domain.Common.Errors;
using SourceSafe.Domain.Entities;
using System.Net;

namespace SourceSafe.Application.Services.FileSerices.Commands.AddFile;

public class AddFileCommandHandler(
    IFileRepository fileRepository,
    IUserRepository userRepository,
    IDateTimeProvider dateTimeProvider):
    IRequestHandler<AddFileCommand, ErrorOr<AddFileResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<ErrorOr<AddFileResult>> Handle(AddFileCommand request, CancellationToken cancellationToken)
    {
        if(_fileRepository.DuplicateName(request.Name).Result)
        {
            return Errors.File.DuplicateName;
        }
        var group = await _fileRepository.GetGroup(request.GroupId);
        if(group is null)
        {
            return Errors.Group.NoGroup;
        }
        var file = new Domain.Entities.File()
        {
            Name = request.Name,
            Path = request.Path,
            Group = group,
            Reserved = false
        };
        await _fileRepository.Add(file);
        var user = _userRepository.GetUserById(request.UserId).Result;
        if(user is null)
        {
            return Errors.User.NoUser;
        }
        var backup = new Backup()
        {
            File = file,
            BackupPath = file.Path,
            User = user,
            Date = _dateTimeProvider.Now
        };
        await _fileRepository.AddBackup(backup);
        return new AddFileResult(
            (HttpStatusCode)StatusCodes.Status201Created,
            $"{file.Name} has been Added to the Group");
    }
}
