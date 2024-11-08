﻿using AutoMapper;
using SourceSafe.Application.Services.UserServices.Commands.Register;
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
    }
}
