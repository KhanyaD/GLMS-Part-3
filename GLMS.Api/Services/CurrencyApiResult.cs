namespace GLMS.Api.Services;

public class CurrencyApiResult
{
    public bool Success { get; set; }
    public decimal Rate { get; set; }
    public string Source { get; set; } = "Live";
    public string? ErrorMessage { get; set; }
}
