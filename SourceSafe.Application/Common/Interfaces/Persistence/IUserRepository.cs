using SourceSafe.Domain.Entities;

namespace SourceSafe.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    Task Add(User user);
    Task<bool> CheckPassword (string email, string password);
    Task<User?> GetUserById(int id);
}
