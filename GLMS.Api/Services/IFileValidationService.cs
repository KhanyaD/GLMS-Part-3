using Microsoft.AspNetCore.Http;

namespace GLMS.Api.Services;

public interface IFileValidationService
{
    void ValidatePdf(IFormFile? file);
}
