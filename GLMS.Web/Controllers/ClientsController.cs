using GLMS.Web.Models;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GLMS.Web.Controllers;

public class ClientsController : Controller
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApiTokenService _tokenService;

    public ClientsController(IHttpClientFactory httpClientFactory, IApiTokenService tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }

    // GET: Clients
    public async Task<IActionResult> Index()
    {
        var client = await CreateAuthorizedClientAsync();
        var clients = await client.GetFromJsonAsync<List<Client>>("api/clients", JsonOptions) ?? [];

        return View(clients);
    }

    // GET: Clients/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var apiClient = await CreateAuthorizedClientAsync();
        var client = await apiClient.GetFromJsonAsync<Client>($"api/clients/{id.Value}", JsonOptions);

        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // GET: Clients/Create
    public IActionResult Create()
    {
        return View(new Client());
    }

    // POST: Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client client)
    {
        if (!ModelState.IsValid)
        {
            return View(client);
        }

        var apiClient = await CreateAuthorizedClientAsync();
        var response = await apiClient.PostAsJsonAsync("api/clients", client);
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Failed to create client via API.");
            return View(client);
        }

        TempData["Success"] = "Client created successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Clients/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var apiClient = await CreateAuthorizedClientAsync();
        var client = await apiClient.GetFromJsonAsync<Client>($"api/clients/{id.Value}", JsonOptions);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: Clients/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Client client)
    {
        if (id != client.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(client);
        }

        var apiClient = await CreateAuthorizedClientAsync();
        var response = await apiClient.PutAsJsonAsync($"api/clients/{id}", client);
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Failed to update client via API.");
            return View(client);
        }

        TempData["Success"] = "Client updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    // GET: Clients/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var apiClient = await CreateAuthorizedClientAsync();
        var client = await apiClient.GetFromJsonAsync<Client>($"api/clients/{id.Value}", JsonOptions);

        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: Clients/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var apiClient = await CreateAuthorizedClientAsync();
        var response = await apiClient.DeleteAsync($"api/clients/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        TempData["Success"] = "Client deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<HttpClient> CreateAuthorizedClientAsync()
    {
        var client = _httpClientFactory.CreateClient("BackendApi");
        var token = await _tokenService.GetAccessTokenAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
