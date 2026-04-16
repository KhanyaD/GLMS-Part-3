using System.ComponentModel.DataAnnotations;
using GLMS.Web.Models.Enums;

namespace GLMS.Web.Models;

public class ServiceRequest
{
    public int Id { get; set; }

    [Required]
    public int ContractId { get; set; }

    public Contract? Contract { get; set; }

    [Required, StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 999999999)]
    public decimal CostUsd { get; set; }

    [Range(0.01, 999999999)]
    public decimal LocalCostZar { get; set; }

    [Range(0.01, 999999999)]
    public decimal ExchangeRateUsed { get; set; }

    [Required]
    public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Pending;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
