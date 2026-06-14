using GLMS.Api.Contracts;
using GLMS.Api.Data;
using GLMS.Api.Models;
using GLMS.Api.Models.Enums;
using GLMS.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Api.Controllers;

[ApiController]
[Route("api/contracts")]
[Authorize]
public class ContractsController : ControllerBase
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

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] ContractStatus? status,
        CancellationToken cancellationToken)
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

        var contracts = await query
            .OrderByDescending(c => c.StartDate)
            .Select(c => new Contract
            {
                Id = c.Id,
                ClientId = c.ClientId,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                ServiceLevel = c.ServiceLevel,
                SignedAgreementFileName = c.SignedAgreementFileName,
                SignedAgreementPath = c.SignedAgreementPath,
                Client = c.Client == null ? null : new Client
                {
                    Id = c.Client.Id,
                    Name = c.Client.Name,
                    ContactDetails = c.Client.ContactDetails,
                    Region = c.Client.Region
                }
            })
            .ToListAsync(cancellationToken);

        return Ok(contracts);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.ServiceRequests)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contract is null)
        {
            return NotFound();
        }

        return Ok(new Contract
        {
            Id = contract.Id,
            ClientId = contract.ClientId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Status = contract.Status,
            ServiceLevel = contract.ServiceLevel,
            SignedAgreementFileName = contract.SignedAgreementFileName,
            SignedAgreementPath = contract.SignedAgreementPath,
            Client = contract.Client == null ? null : new Client
            {
                Id = contract.Client.Id,
                Name = contract.Client.Name,
                ContactDetails = contract.Client.ContactDetails,
                Region = contract.Client.Region
            },
            ServiceRequests = contract.ServiceRequests.Select(sr => new ServiceRequest
            {
                Id = sr.Id,
                ContractId = sr.ContractId,
                Description = sr.Description,
                CostUsd = sr.CostUsd,
                LocalCostZar = sr.LocalCostZar,
                ExchangeRateUsed = sr.ExchangeRateUsed,
                Status = sr.Status,
                CreatedAtUtc = sr.CreatedAtUtc
            }).ToList()
        });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateContractRequest request, CancellationToken cancellationToken)
    {
        if (request.EndDate < request.StartDate)
        {
            return BadRequest("End date must be on or after the start date.");
        }

        _fileValidationService.ValidatePdf(request.SignedAgreement);

        var contract = new Contract
        {
            ClientId = request.ClientId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            ServiceLevel = request.ServiceLevel
        };

        if (request.SignedAgreement is not null)
        {
            var fileResult = await _contractFileService.SaveSignedAgreementAsync(request.SignedAgreement, cancellationToken);
            contract.SignedAgreementFileName = fileResult.FileName;
            contract.SignedAgreementPath = fileResult.RelativePath;
        }

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = contract.Id }, contract);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> PatchStatus(int id, [FromBody] UpdateContractStatusRequest request, CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (contract is null)
        {
            return NotFound();
        }

        contract.Status = request.Status;
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(contract);
    }

    [HttpGet("{id:int}/agreement")]
    public async Task<IActionResult> DownloadAgreement(int id, CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contract?.SignedAgreementPath is null)
        {
            return NotFound();
        }

        var physicalPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            contract.SignedAgreementPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

        if (!System.IO.File.Exists(physicalPath))
        {
            return NotFound();
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(physicalPath, cancellationToken);
        return File(fileBytes, "application/pdf", contract.SignedAgreementFileName ?? "agreement.pdf");
    }
}
