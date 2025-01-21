using AutoMapper;
using SourceSafe.Application.Services.FileSerices.Commands.AddFile;
using SourceSafe.Application.Services.FileSerices.Commands.Check_in;
using SourceSafe.Application.Services.FileSerices.Commands.Check_out;
using SourceSafe.Application.Services.FileSerices.Commands.DeleteFile;
using SourceSafe.Application.Services.FileSerices.Queries.GetGroupFiles;
using SourceSafe.Application.Services.ReportServices.Queries.GetFileReport;
using SourceSafe.Application.Services.ReportServices.Queries.GetUsersReport;
using SourceSafe.Contracts.File;
using SourceSafe.Contracts.Group;

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
        CreateMap<GetGroupFilesResult, GetUserGroupsResponse>()
            .ForMember(dest => dest.Items,
            opt => opt.MapFrom(src => src.Items));
        CreateMap<DeleteFileResult, Check_inResponse>();
        CreateMap<GetFileReportResult, GetUserGroupsResponse>()
            .ForMember(dest => dest.Items,
            opt => opt.MapFrom(src => src.Items));
        CreateMap<GetUsersReportRequest, GetUsersReportQuery>();
        CreateMap<GetUsersReportResult, GetUserGroupsResponse>()
            .ForMember(dest => dest.Items,
            opt => opt.MapFrom(src => src.Items));
    }
}
