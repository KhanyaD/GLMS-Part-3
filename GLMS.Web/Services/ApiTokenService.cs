using System.Net.Http.Json;

namespace GLMS.Web.Services;

public class ApiTokenService : IApiTokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private string? _cachedToken;
    private DateTime _expiresAtUtc;

    public ApiTokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(_cachedToken) && DateTime.UtcNow < _expiresAtUtc.AddMinutes(-1))
        {
            return _cachedToken;
        }

        var username = _configuration["BackendApi:Username"] ?? "glms-admin";
        var password = _configuration["BackendApi:Password"] ?? "P@ssw0rd!";

        var client = _httpClientFactory.CreateClient("BackendApi");
        using var response = await client.PostAsJsonAsync("api/auth/token", new
        {
            Username = username,
            Password = password
        }, cancellationToken);

        response.EnsureSuccessStatusCode();

        var tokenPayload = await response.Content.ReadFromJsonAsync<TokenPayload>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Token response was empty.");

        _cachedToken = tokenPayload.AccessToken;
        _expiresAtUtc = tokenPayload.ExpiresAtUtc;

        return _cachedToken;
    }

    private sealed class TokenPayload
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
    }
}
