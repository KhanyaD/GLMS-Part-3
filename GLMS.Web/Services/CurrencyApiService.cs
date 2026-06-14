using System.Text.Json;

namespace GLMS.Web.Services;

public class CurrencyApiService(HttpClient httpClient, IConfiguration configuration, ILogger<CurrencyApiService> logger) : ICurrencyApiService
{
    public async Task<CurrencyApiResult> GetUsdToZarRateAsync(CancellationToken cancellationToken = default)
    {
        var endpoint = configuration["CurrencyApi:LatestEndpoint"] ?? "latest?base=USD&symbols=ZAR";
        var apiKey = configuration["CurrencyApi:ApiKey"];

        // Add API key to endpoint if provided
        if (!string.IsNullOrEmpty(apiKey))
        {
            endpoint += endpoint.Contains('?') ? "&" : "?";
            endpoint += $"access_key={apiKey}";
        }

        try
        {
            using var response = await httpClient.GetAsync(endpoint, cancellationToken);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var data = await JsonSerializer.DeserializeAsync<ExchangeRateResponse>(stream, cancellationToken: cancellationToken);

            var rate = data?.Rates != null && data.Rates.TryGetValue("ZAR", out var zar)
                ? zar
                : 0m;

            if (rate <= 0)
            {
                return new CurrencyApiResult
                {
                    Success = false,
                    ErrorMessage = "The currency API returned an invalid USD to ZAR rate."
                };
            }

            return new CurrencyApiResult
            {
                Success = true,
                Rate = rate
            };
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Falling back to the default exchange rate.");
            return new CurrencyApiResult
            {
                Success = true,
                Rate = 18.50m,
                Source = "Fallback",
                ErrorMessage = "Live API unavailable. Fallback rate used."
            };
        }
    }
}
