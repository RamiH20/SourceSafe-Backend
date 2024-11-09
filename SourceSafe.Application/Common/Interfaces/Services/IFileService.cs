using Microsoft.AspNetCore.Http;

namespace SourceSafe.Application.Common.Interfaces.Services;

public interface IFileService
{
    public string SaveFile(IFormFile formFile);
}
