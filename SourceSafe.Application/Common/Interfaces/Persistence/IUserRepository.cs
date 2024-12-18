﻿using SourceSafe.Application.Common.DTOs;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    Task Add(User user);
    Task<bool> CheckPassword (string email, string password);
    Task<User?> GetUserById(int id);
    Task<List<UserDTO>> GetAllUsers(int id, string Search);
    Task AddRefreshToken(User user, RefreshToken refreshToken);
    Task<User?> GetUserByToken(string token);
    Task<string?> GetUserRole(int id);
}
