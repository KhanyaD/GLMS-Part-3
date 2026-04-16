using Microsoft.AspNetCore.Http;

namespace GLMS.Web.Services;

public interface IContractFileService
{
    Task<(string FileName, string RelativePath)> SaveSignedAgreementAsync(IFormFile file, CancellationToken cancellationToken = default);
}
