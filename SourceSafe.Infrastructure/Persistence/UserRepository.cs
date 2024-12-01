using Microsoft.EntityFrameworkCore;
using SourceSafe.Application.Common.DTOs;
using SourceSafe.Application.Common.Interfaces.Persistence;
using SourceSafe.Domain.Entities;
using SourceSafe.Infrastructure.Data;

namespace SourceSafe.Infrastructure.Persistence;

public class UserRepository(SourceSafeDbContext dbContext) : IUserRepository
{
    private readonly SourceSafeDbContext _dbContext = dbContext;
    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(x => x.Email == email);
    }
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<bool> CheckPassword(string email, string password)
    {
        return await _dbContext.Users.Where(x => x.Email == email).AnyAsync(x => x.Password == password);
    }
    public Task<User?> GetUserById(int id)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<List<UserDTO>> GetAllUsers(int id, string Search)
    {
        if(Search is null)
        {
            return await _dbContext.Users
            .Where(x => x.Id != id && x.Role.Id != 1)
            .Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
            }).ToListAsync();
        }
        else
        {
            return await _dbContext.Users
            .Where(x => x.Id != id && x.Role.Id != 1
            && (x.Name.Contains(Search)|| x.Email.Contains(Search)))
            .Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
            }).ToListAsync();
        }
    }
    public async Task AddRefreshToken(User user, RefreshToken refreshToken)
    {
        user.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<User?> GetUserByToken(string token)
    {
        return await _dbContext.Users
            .SingleOrDefaultAsync(x => x.RefreshTokens
            .Any(t => t.Token == token));
    }
    public async Task<string?> GetUserRole(int id)
    {
        return await _dbContext.Roles.Where(x => x.Id == id)
            .Select(x => x.Name).FirstOrDefaultAsync();
    }
}
