using Microsoft.AspNetCore.Http;

namespace GLMS.Api.Services;

public class ContractFileService : IContractFileService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;

    public ContractFileService(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
    }

    public async Task<(string FileName, string RelativePath)> SaveSignedAgreementAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var configuredFolder = _configuration["FileStorage:ContractUploadFolder"] ?? "wwwroot/uploads/contracts";
        var rootPath = _environment.ContentRootPath;
        var targetFolder = Path.Combine(rootPath, configuredFolder.Replace('/', Path.DirectorySeparatorChar));

        Directory.CreateDirectory(targetFolder);

        var safeName = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";
        var fullPath = Path.Combine(targetFolder, safeName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        var relativePath = $"/uploads/contracts/{safeName}";
        return (safeName, relativePath);
    }
}
