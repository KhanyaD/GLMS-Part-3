using GLMS.Api.Contracts;
using GLMS.Api.Data;
using GLMS.Api.Models;
using GLMS.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Api.Controllers;

[ApiController]
[Route("api/service-requests")]
[Authorize]
public class ServiceRequestsController : ControllerBase
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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var requests = await _context.ServiceRequests
            .Include(sr => sr.Contract)
            .ThenInclude(c => c!.Client)
            .OrderByDescending(sr => sr.CreatedAtUtc)
            .Select(sr => new ServiceRequest
            {
                Id = sr.Id,
                ContractId = sr.ContractId,
                Description = sr.Description,
                CostUsd = sr.CostUsd,
                ExchangeRateUsed = sr.ExchangeRateUsed,
                LocalCostZar = sr.LocalCostZar,
                Status = sr.Status,
                CreatedAtUtc = sr.CreatedAtUtc,
                Contract = sr.Contract == null ? null : new Contract
                {
                    Id = sr.Contract.Id,
                    ClientId = sr.Contract.ClientId,
                    StartDate = sr.Contract.StartDate,
                    EndDate = sr.Contract.EndDate,
                    Status = sr.Contract.Status,
                    ServiceLevel = sr.Contract.ServiceLevel,
                    Client = sr.Contract.Client == null ? null : new Client
                    {
                        Id = sr.Contract.Client.Id,
                        Name = sr.Contract.Client.Name,
                        ContactDetails = sr.Contract.Client.ContactDetails,
                        Region = sr.Contract.Client.Region
                    }
                }
            })
            .ToListAsync(cancellationToken);

        return Ok(requests);
    }

    [HttpGet("usd-zar-rate")]
    public async Task<IActionResult> GetUsdToZarRate(CancellationToken cancellationToken)
    {
        var result = await _currencyApiService.GetUsdToZarRateAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceRequestRequest request, CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.Id == request.ContractId, cancellationToken);

        if (contract is null)
        {
            return BadRequest("Selected contract does not exist.");
        }

        if (!_workflowService.CanCreateRequest(contract, out var workflowError))
        {
            return BadRequest(workflowError ?? "This contract cannot accept service requests.");
        }

        var exchangeRate = request.ExchangeRateUsed;
        if (exchangeRate <= 0)
        {
            var rateResult = await _currencyApiService.GetUsdToZarRateAsync(cancellationToken);
            exchangeRate = rateResult.Rate;
        }

        var localCost = _currencyCalculator.ConvertUsdToZar(request.CostUsd, exchangeRate);

        var serviceRequest = new ServiceRequest
        {
            ContractId = request.ContractId,
            Description = request.Description,
            CostUsd = request.CostUsd,
            ExchangeRateUsed = exchangeRate,
            LocalCostZar = localCost,
            Status = request.Status
        };

        _context.ServiceRequests.Add(serviceRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetAll), new { id = serviceRequest.Id }, serviceRequest);
    }
}
