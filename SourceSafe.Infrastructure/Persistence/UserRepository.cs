using Microsoft.EntityFrameworkCore;
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
}
