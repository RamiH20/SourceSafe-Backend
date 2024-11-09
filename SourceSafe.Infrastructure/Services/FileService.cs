using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SourceSafe.Application.Common.Interfaces.Services;

namespace SourceSafe.Infrastructure.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    private readonly IWebHostEnvironment _environment = environment;
    private static string GetUniqueFileName(string fileName)
    {
        var guid = Guid.NewGuid().ToString();
        var extension = Path.GetExtension(fileName).ToLower();
        return guid + extension;
    }

    public string SaveFile(IFormFile formFile)
    {

        var uniqueFileName = GetUniqueFileName(formFile.FileName);
        var filePath = Path.Combine(_environment.ContentRootPath, "Uploads");

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        var fileWithPath = Path.Combine(filePath, uniqueFileName);
        var stream = new FileStream(fileWithPath, FileMode.Create);
        formFile.CopyTo(stream);
        stream.Close();
        var name = "/Uploads/" + Path.GetFileName(uniqueFileName);

        return name;
    }
}
