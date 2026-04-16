using GLMS.Web.Models;
using GLMS.Web.Models.Enums;

namespace GLMS.Web.Services;

public class ServiceRequestWorkflowService : IServiceRequestWorkflowService
{
    public bool CanCreateRequest(Contract contract, out string? errorMessage)
    {
        if (contract.Status == ContractStatus.Expired || contract.Status == ContractStatus.OnHold)
        {
            errorMessage = "Service requests cannot be created for contracts that are Expired or On Hold.";
            return false;
        }

        if (contract.EndDate.Date < DateTime.Today)
        {
            errorMessage = "Service requests cannot be created for contracts past the end date.";
            return false;
        }

        errorMessage = null;
        return true;
    }
}
