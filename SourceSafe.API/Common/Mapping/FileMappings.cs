using AutoMapper;
using SourceSafe.Application.Services.FileSerices.Commands.AddFile;
using SourceSafe.Application.Services.FileSerices.Commands.Check_in;
using SourceSafe.Application.Services.FileSerices.Commands.Check_out;
using SourceSafe.Application.Services.UserServices.Commands.Register;
using SourceSafe.Application.Services.UserServices.Queries.Login;
using SourceSafe.Contracts.File;
using SourceSafe.Contracts.User;

namespace SourceSafe.API.Common.Mapping;

public class FileMappings : Profile
{
    public FileMappings()
    {
        CreateMap<AddFileResult, AddFileResponse>();
        CreateMap<Check_inRequest, Check_inCommand>();
        CreateMap<Check_inResult, Check_inResponse>();
        CreateMap<Check_outRequest, Check_outCommand>();
        CreateMap<Check_outResult, Check_outResponse>();
    }
}
