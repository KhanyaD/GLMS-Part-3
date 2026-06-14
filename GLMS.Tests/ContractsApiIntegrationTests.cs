using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using GLMS.Api.Models;
using GLMS.Api.Models.Enums;
using Xunit;

namespace GLMS.Tests;

public class ContractsApiIntegrationTests : IClassFixture<GlmsApiFactory>
{
    private readonly HttpClient _client;

    public ContractsApiIntegrationTests(GlmsApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetContracts_ReturnsSuccessStatusCode()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/contracts");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var contracts = await response.Content.ReadFromJsonAsync<List<Contract>>();
        contracts.Should().NotBeNull();
    }

    [Fact]
    public async Task GetContracts_WithFilters_ReturnsFilteredResults()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var fromDate = DateTime.UtcNow.AddDays(-30);
        var status = ContractStatus.Active;

        // Act
        var response = await _client.GetAsync(
            $"/api/contracts?fromDate={fromDate:O}&status={status}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var contracts = await response.Content.ReadFromJsonAsync<List<Contract>>();
        contracts.Should().NotBeNull();
        contracts.Should().AllSatisfy(c =>
        {
            c.StartDate.Should().BeOnOrAfter(fromDate);
            c.Status.Should().Be(status);
        });
    }

    [Fact]
    public async Task GetContractById_ExistingId_ReturnsContract()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        const int existingId = 1;

        // Act
        var response = await _client.GetAsync($"/api/contracts/{existingId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var contract = await response.Content.ReadFromJsonAsync<Contract>();
        contract.Should().NotBeNull();
        contract!.Id.Should().Be(existingId);
    }

    [Fact]
    public async Task GetContractById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        const int nonExistingId = 99999;

        // Act
        var response = await _client.GetAsync($"/api/contracts/{nonExistingId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PatchContractStatus_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        const int contractId = 1;
        var updateRequest = new { Status = ContractStatus.OnHold };
        var content = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PatchAsync($"/api/contracts/{contractId}/status", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedContract = await response.Content.ReadFromJsonAsync<Contract>();
        updatedContract.Should().NotBeNull();
        updatedContract!.Status.Equals(ContractStatus.OnHold).Should().BeTrue();
    }

    [Fact]
    public async Task GetContracts_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange - No authentication header set

        // Act
        var response = await _client.GetAsync("/api/contracts");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateContract_ValidData_ReturnsCreated()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var formData = new MultipartFormDataContent
        {
            { new StringContent("1"), "ClientId" },
            { new StringContent(DateTime.UtcNow.ToString("O")), "StartDate" },
            { new StringContent(DateTime.UtcNow.AddMonths(12).ToString("O")), "EndDate" },
            { new StringContent("0"), "Status" },
            { new StringContent("Premium"), "ServiceLevel" }
        };

        // Act
        var response = await _client.PostAsync("/api/contracts", formData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
        // Optionally, you can assert on the value:
        // response.Headers.Location!.ToString().Should().NotBeNullOrEmpty();
    }

    private async Task<string> GetAuthTokenAsync()
    {
        // Use the seeded API credentials configured in appsettings for integration tests.
        var loginRequest = new
        {
            Username = "glms-admin",
            Password = "P@ssw0rd!"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/token", loginRequest);
        response.EnsureSuccessStatusCode();

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return tokenResponse!.AccessToken;
    }

    private class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}