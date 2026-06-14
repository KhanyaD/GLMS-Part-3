using System.ComponentModel.DataAnnotations;
using GLMS.Api.Models.Enums;

namespace GLMS.Api.Models;

public class Contract
{
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Required]
    public ContractStatus Status { get; set; }

    [Required, StringLength(100)]
    public string ServiceLevel { get; set; } = string.Empty;

    [StringLength(255)]
    public string? SignedAgreementFileName { get; set; }

    [StringLength(500)]
    public string? SignedAgreementPath { get; set; }

    public Client? Client { get; set; }

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}
