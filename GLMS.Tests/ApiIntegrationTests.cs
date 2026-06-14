using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GLMS.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace GLMS.Tests;

public class ApiIntegrationTests : IClassFixture<GlmsApiFactory>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(GlmsApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetContracts_ReturnsOk_AndJsonNotNull()
    {
        var tokenResponse = await _client.PostAsJsonAsync("/api/auth/token", new
        {
            Username = "glms-admin",
            Password = "P@ssw0rd!"
        });

        tokenResponse.EnsureSuccessStatusCode();
        var tokenPayload = await tokenResponse.Content.ReadFromJsonAsync<TokenPayload>();
        Assert.NotNull(tokenPayload);
        Assert.False(string.IsNullOrWhiteSpace(tokenPayload!.AccessToken));

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenPayload.AccessToken);

        var response = await _client.GetAsync("/api/contracts");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(json));
    }

    private sealed class TokenPayload
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}

public class GlmsApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("GlmsApiIntegrationDb"));

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
            SeedData.InitializeAsync(db).GetAwaiter().GetResult();
        });
    }
}
