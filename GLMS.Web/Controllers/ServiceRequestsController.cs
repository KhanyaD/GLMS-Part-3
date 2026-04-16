using GLMS.Web.Data;
using GLMS.Web.Models;
using GLMS.Web.ViewModels;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Controllers;

public class ServiceRequestsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrencyApiService _currencyApiService;
    private readonly ICurrencyCalculator _currencyCalculator;
    private readonly IServiceRequestWorkflowService _workflowService;

    public ServiceRequestsController(
        ApplicationDbContext context,
        ICurrencyApiService currencyApiService,
        ICurrencyCalculator currencyCalculator,
        IServiceRequestWorkflowService workflowService)
    {
        _context = context;
        _currencyApiService = currencyApiService;
        _currencyCalculator = currencyCalculator;
        _workflowService = workflowService;
    }

    public async Task<IActionResult> Index()
    {
        var requests = await _context.ServiceRequests
            .Include(sr => sr.Contract)
            .ThenInclude(c => c!.Client)
            .OrderByDescending(sr => sr.CreatedAtUtc)
            .ToListAsync();

        return View(requests);
    }

    public async Task<IActionResult> Create()
    {
        var contracts = await _context.Contracts
            .Include(c => c.Client)
            .OrderByDescending(c => c.StartDate)
            .ToListAsync();

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
        var result = await _currencyApiService.GetUsdToZarRateAsync();
        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceRequestCreateViewModel model)
    {
        model.ActiveContracts = await _context.Contracts
            .Include(c => c.Client)
            .OrderByDescending(c => c.StartDate)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"#{c.Id} - {c.Client!.Name} [{c.Status}]"
            })
            .ToListAsync();

        var contract = await _context.Contracts
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.Id == model.ContractId);

        if (contract is null)
        {
            ModelState.AddModelError(nameof(model.ContractId), "Selected contract does not exist.");
        }
        else if (!_workflowService.CanCreateRequest(contract, out var workflowError))
        {
            ModelState.AddModelError(nameof(model.ContractId), workflowError ?? "This contract cannot accept service requests.");
        }

        if (model.ExchangeRateUsed <= 0)
        {
            var rateResult = await _currencyApiService.GetUsdToZarRateAsync();
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

        var request = new ServiceRequest
        {
            ContractId = model.ContractId,
            Description = model.Description,
            CostUsd = model.CostUsd,
            ExchangeRateUsed = model.ExchangeRateUsed,
            LocalCostZar = model.LocalCostZar,
            Status = model.Status
        };

        _context.ServiceRequests.Add(request);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Service request created successfully.";
        return RedirectToAction(nameof(Index));
    }
}
