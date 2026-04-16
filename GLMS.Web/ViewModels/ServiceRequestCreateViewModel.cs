using System.ComponentModel.DataAnnotations;
using GLMS.Web.Models;
using GLMS.Web.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GLMS.Web.ViewModels;

public class ServiceRequestCreateViewModel
{
    [Required]
    [Display(Name = "Contract")]
    public int ContractId { get; set; }

    [Required, StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 999999999)]
    [Display(Name = "Cost (USD)")]
    public decimal CostUsd { get; set; }

    [Display(Name = "Exchange Rate (USD → ZAR)")]
    public decimal ExchangeRateUsed { get; set; }

    [Display(Name = "Local Cost (ZAR)")]
    public decimal LocalCostZar { get; set; }

    public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Pending;

    public IEnumerable<SelectListItem> ActiveContracts { get; set; } = Enumerable.Empty<SelectListItem>();
    public Contract? SelectedContract { get; set; }
}
