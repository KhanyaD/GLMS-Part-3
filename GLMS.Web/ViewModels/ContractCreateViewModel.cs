using System.ComponentModel.DataAnnotations;
using GLMS.Web.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GLMS.Web.ViewModels;

public class ContractCreateViewModel
{
    [Required]
    [Display(Name = "Client")]
    public int ClientId { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [Required, DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(12);

    [Required]
    public ContractStatus Status { get; set; } = ContractStatus.Draft;

    [Required, StringLength(100)]
    [Display(Name = "Service Level")]
    public string ServiceLevel { get; set; } = string.Empty;

    [Display(Name = "Signed Agreement (PDF)")]
    public IFormFile? SignedAgreement { get; set; }

    public IEnumerable<SelectListItem> Clients { get; set; } = Enumerable.Empty<SelectListItem>();
}
