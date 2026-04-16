using Microsoft.AspNetCore.Http;

namespace GLMS.Web.Services;

public class FileValidationService : IFileValidationService
{
    private static readonly string[] AllowedExtensions = [".pdf"];
    private const long MaxFileSizeBytes = 10 * 1024 * 1024;

    public void ValidatePdf(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            return;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
        {
            throw new InvalidOperationException("Only PDF files are allowed.");
        }

        if (file.Length > MaxFileSizeBytes)
        {
            throw new InvalidOperationException("The uploaded PDF exceeds the 10 MB limit.");
        }
    }
}
