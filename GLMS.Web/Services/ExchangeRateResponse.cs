using System.Text.Json.Serialization;

namespace GLMS.Web.Services;

public class ExchangeRateResponse
{
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    [JsonPropertyName("rates")]
    public Dictionary<string, decimal>? Rates { get; set; }
}
