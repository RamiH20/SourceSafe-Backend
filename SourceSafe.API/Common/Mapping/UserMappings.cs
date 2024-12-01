using AutoMapper;
using SourceSafe.Application.Services.UserServices.Commands.RefreshToken;
using SourceSafe.Application.Services.UserServices.Commands.Register;
using SourceSafe.Application.Services.UserServices.Queries.GetAllUsers;
using SourceSafe.Application.Services.UserServices.Queries.Login;
using SourceSafe.Contracts.User;

namespace SourceSafe.API.Common.Mapping;

public class UserMappings : Profile
{
    public UserMappings() 
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<RegisterResult, RegisterResponse>();
        CreateMap<LoginRequest, LoginQuery>();
        CreateMap<LoginResult, LoginResponse>();
        CreateMap<GetAllUsersRequest, GetAllUsersQuery>();
        CreateMap<GetAllUsersResult, GetAllUsersResponse>()
            .ForMember(dest => dest.Items, 
            opt => opt.MapFrom(src => src.Users));
        CreateMap<RefreshTokenResult, LoginResponse>();
    }
}
