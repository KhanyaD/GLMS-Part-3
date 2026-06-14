using GLMS.Web.Models;
using GLMS.Web.ViewModels;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GLMS.Web.Controllers;

public class ServiceRequestsController : Controller
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApiTokenService _tokenService;
    private readonly ICurrencyCalculator _currencyCalculator;

    public ServiceRequestsController(
        IHttpClientFactory httpClientFactory,
        IApiTokenService tokenService,
        ICurrencyCalculator currencyCalculator)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
        _currencyCalculator = currencyCalculator;
    }

    public async Task<IActionResult> Index()
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var requests = await apiClient.GetFromJsonAsync<List<ServiceRequest>>("api/service-requests", JsonOptions) ?? [];

        return View(requests.OrderByDescending(sr => sr.CreatedAtUtc).ToList());
    }

    public async Task<IActionResult> Create()
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var contracts = await apiClient.GetFromJsonAsync<List<Contract>>("api/contracts", JsonOptions) ?? [];

        var vm = new ServiceRequestCreateViewModel
        {
            ActiveContracts = contracts.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"#{c.Id} - {c.Client!.Name} [{c.Status}]"
            })
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsdToZarRate()
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var result = await apiClient.GetFromJsonAsync<CurrencyApiResultDto>("api/service-requests/usd-zar-rate", JsonOptions)
            ?? new CurrencyApiResultDto { Success = false, ErrorMessage = "Unable to retrieve exchange rate." };
        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceRequestCreateViewModel model)
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var contracts = await apiClient.GetFromJsonAsync<List<Contract>>("api/contracts", JsonOptions) ?? [];

        model.ActiveContracts = contracts
            .OrderByDescending(c => c.StartDate)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"#{c.Id} - {c.Client!.Name} [{c.Status}]"
            })
            .ToList();

        if (model.ExchangeRateUsed <= 0)
        {
            var rateResult = await apiClient.GetFromJsonAsync<CurrencyApiResultDto>("api/service-requests/usd-zar-rate", JsonOptions)
                ?? new CurrencyApiResultDto { Success = false };
            model.ExchangeRateUsed = rateResult.Rate;
        }

        if (model.CostUsd > 0 && model.ExchangeRateUsed > 0)
        {
            model.LocalCostZar = _currencyCalculator.ConvertUsdToZar(model.CostUsd, model.ExchangeRateUsed);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var payload = new
        {
            model.ContractId,
            model.Description,
            model.CostUsd,
            model.ExchangeRateUsed,
            model.Status
        };

        using var response = await apiClient.PostAsJsonAsync("api/service-requests", payload);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(nameof(model.ContractId), string.IsNullOrWhiteSpace(error) ? "Unable to create service request." : error);
            return View(model);
        }

        TempData["Success"] = "Service request created successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<HttpClient> CreateAuthorizedClientAsync(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("BackendApi");
        var token = await _tokenService.GetAccessTokenAsync(cancellationToken);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    private sealed class CurrencyApiResultDto
    {
        public bool Success { get; set; }
        public decimal Rate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
