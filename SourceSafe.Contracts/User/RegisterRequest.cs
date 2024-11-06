using System.ComponentModel.DataAnnotations;

namespace SourceSafe.Contracts.User;

public class RegisterRequest
{
    public string Name { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
