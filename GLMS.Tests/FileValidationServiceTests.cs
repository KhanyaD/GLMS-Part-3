using GLMS.Web.Services;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GLMS.Tests;

public class FileValidationServiceTests
{
    [Fact]
    public void ValidatePdf_AllowsPdfFiles()
    {
        var service = new FileValidationService();
        var stream = new MemoryStream(new byte[] { 1, 2, 3, 4 });
        IFormFile file = new FormFile(stream, 0, stream.Length, "Data", "agreement.pdf");

        var exception = Record.Exception(() => service.ValidatePdf(file));

        Assert.Null(exception);
    }

    [Fact]
    public void ValidatePdf_ThrowsForExeFiles()
    {
        var service = new FileValidationService();
        var stream = new MemoryStream(new byte[] { 1, 2, 3, 4 });
        IFormFile file = new FormFile(stream, 0, stream.Length, "Data", "malware.exe");

        var exception = Assert.Throws<InvalidOperationException>(() => service.ValidatePdf(file));

        Assert.Equal("Only PDF files are allowed.", exception.Message);
    }
}
