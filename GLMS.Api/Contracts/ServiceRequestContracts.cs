using GLMS.Api.Models.Enums;

namespace GLMS.Api.Contracts;

public class CreateServiceRequestRequest
{
    public int ContractId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal CostUsd { get; set; }
    public decimal ExchangeRateUsed { get; set; }
    public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Pending;
}
