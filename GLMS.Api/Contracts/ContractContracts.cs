using GLMS.Api.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace GLMS.Api.Contracts;

public class CreateContractRequest
{
    public int ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public string ServiceLevel { get; set; } = string.Empty;
    public IFormFile? SignedAgreement { get; set; }
}

public class UpdateContractStatusRequest
{
    public ContractStatus Status { get; set; }
}
