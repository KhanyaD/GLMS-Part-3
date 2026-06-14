using Microsoft.AspNetCore.Http;

namespace GLMS.Api.Services;

public interface IContractFileService
{
    Task<(string FileName, string RelativePath)> SaveSignedAgreementAsync(IFormFile file, CancellationToken cancellationToken = default);
}
