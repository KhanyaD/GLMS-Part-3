using System.Text.Json.Serialization;

namespace GLMS.Api.Services;

public class ExchangeRateResponse
{
    [JsonPropertyName("rates")]
    public Dictionary<string, decimal>? Rates { get; set; }
}
