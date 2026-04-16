using GLMS.Web.Data;
using GLMS.Web.Models;
using GLMS.Web.ViewModels;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Controllers;

public class ContractsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IContractFileService _contractFileService;
    private readonly IFileValidationService _fileValidationService;

    public ContractsController(
        ApplicationDbContext context,
        IContractFileService contractFileService,
        IFileValidationService fileValidationService)
    {
        _context = context;
        _contractFileService = contractFileService;
        _fileValidationService = fileValidationService;
    }

    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, GLMS.Web.Models.Enums.ContractStatus? status)
    {
        var query = _context.Contracts
            .Include(c => c.Client)
            .AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(c => c.StartDate >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(c => c.EndDate <= toDate.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(c => c.Status == status.Value);
        }

        var vm = new ContractFilterViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            Status = status,
            Contracts = await query
                .OrderByDescending(c => c.StartDate)
                .ToListAsync()
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

        try
        {
            _fileValidationService.ValidatePdf(model.SignedAgreement);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(nameof(model.SignedAgreement), ex.Message);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var contract = new Contract
        {
            ClientId = model.ClientId,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Status = model.Status,
            ServiceLevel = model.ServiceLevel
        };

        if (model.SignedAgreement is not null)
        {
            var fileResult = await _contractFileService.SaveSignedAgreementAsync(model.SignedAgreement, cancellationToken);
            contract.SignedAgreementFileName = fileResult.FileName;
            contract.SignedAgreementPath = fileResult.RelativePath;
        }

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);

        TempData["Success"] = "Contract created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var contract = await _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.ServiceRequests)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contract is null)
        {
            return NotFound();
        }

        return View(contract);
    }

    public async Task<IActionResult> DownloadAgreement(int id)
    {
        var contract = await _context.Contracts.FindAsync(id);

        if (contract?.SignedAgreementPath is null)
        {
            return NotFound();
        }

        var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", contract.SignedAgreementPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (!System.IO.File.Exists(physicalPath))
        {
            return NotFound();
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(physicalPath);
        return File(fileBytes, "application/pdf", contract.SignedAgreementFileName ?? "agreement.pdf");
    }

    private async Task<IEnumerable<SelectListItem>> GetClientSelectListAsync()
    {
        return await _context.Clients
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} ({c.Region})"
            })
            .ToListAsync();
    }
}
