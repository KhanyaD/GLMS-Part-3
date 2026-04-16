using GLMS.Web.Models;
using GLMS.Web.Models.Enums;

namespace GLMS.Web.ViewModels;

public class ContractFilterViewModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public ContractStatus? Status { get; set; }
    public List<Contract> Contracts { get; set; } = new();
}
