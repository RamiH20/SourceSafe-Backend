using Microsoft.AspNetCore.Http;

namespace SourceSafe.Contracts.File;

public class AddFileRequest
{
    public string Name { get; set; } = null!;
    public int GroupId { get; set; }
    public IFormFile FormFile { get; set; } = null!;
    public int UserId { get; set; }
}
