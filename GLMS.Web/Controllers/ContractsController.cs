using GLMS.Web.Models;
using GLMS.Web.ViewModels;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GLMS.Web.Controllers;

public class ContractsController : Controller
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApiTokenService _tokenService;

    public ContractsController(
        IHttpClientFactory httpClientFactory,
        IApiTokenService tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }

    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, GLMS.Web.Models.Enums.ContractStatus? status)
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var queryParams = new List<string>();

        if (fromDate.HasValue)
        {
            queryParams.Add($"fromDate={fromDate.Value:O}");
        }

        if (toDate.HasValue)
        {
            queryParams.Add($"toDate={toDate.Value:O}");
        }

        if (status.HasValue)
        {
            queryParams.Add($"status={(int)status.Value}");
        }

        var querySuffix = queryParams.Count > 0 ? $"?{string.Join("&", queryParams)}" : string.Empty;
        var contracts = await apiClient.GetFromJsonAsync<List<Contract>>($"api/contracts{querySuffix}", JsonOptions) ?? [];

        var vm = new ContractFilterViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            Status = status,
            Contracts = contracts
                .OrderByDescending(c => c.StartDate)
                .ToList()
        };

        return View(vm);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ContractCreateViewModel
        {
            Clients = await GetClientSelectListAsync()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContractCreateViewModel model, CancellationToken cancellationToken)
    {
        model.Clients = await GetClientSelectListAsync();

        if (model.EndDate < model.StartDate)
        {
            ModelState.AddModelError(nameof(model.EndDate), "End date must be on or after the start date.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var apiClient = await CreateAuthorizedClientAsync();
        using var multipart = new MultipartFormDataContent();
        multipart.Add(new StringContent(model.ClientId.ToString()), nameof(model.ClientId));
        multipart.Add(new StringContent(model.StartDate.ToString("O")), nameof(model.StartDate));
        multipart.Add(new StringContent(model.EndDate.ToString("O")), nameof(model.EndDate));
        multipart.Add(new StringContent(((int)model.Status).ToString()), nameof(model.Status));
        multipart.Add(new StringContent(model.ServiceLevel), nameof(model.ServiceLevel));

        if (model.SignedAgreement is not null)
        {
            var fileContent = new StreamContent(model.SignedAgreement.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.SignedAgreement.ContentType ?? "application/pdf");
            multipart.Add(fileContent, nameof(model.SignedAgreement), model.SignedAgreement.FileName);
        }

        using var response = await apiClient.PostAsync("api/contracts", multipart, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(error) ? "Failed to create contract via API." : error);
            return View(model);
        }

        TempData["Success"] = "Contract created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var contract = await apiClient.GetFromJsonAsync<Contract>($"api/contracts/{id}", JsonOptions);

        if (contract is null)
        {
            return NotFound();
        }

        return View(contract);
    }

    public async Task<IActionResult> DownloadAgreement(int id)
    {
        var apiClient = await CreateAuthorizedClientAsync();
        using var response = await apiClient.GetAsync($"api/contracts/{id}/agreement");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var fileBytes = await response.Content.ReadAsByteArrayAsync();
        var fileName = response.Content.Headers.ContentDisposition?.FileNameStar
            ?? response.Content.Headers.ContentDisposition?.FileName
            ?? "agreement.pdf";

        return File(fileBytes, "application/pdf", fileName.Trim('"'));
    }

    private async Task<IEnumerable<SelectListItem>> GetClientSelectListAsync()
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var clients = await apiClient.GetFromJsonAsync<List<Client>>("api/clients", JsonOptions) ?? [];

        return clients
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} ({c.Region})"
            })
            .ToList();
    }

    private async Task<HttpClient> CreateAuthorizedClientAsync(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("BackendApi");
        var token = await _tokenService.GetAccessTokenAsync(cancellationToken);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
