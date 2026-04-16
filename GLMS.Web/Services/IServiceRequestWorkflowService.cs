using GLMS.Web.Models;

namespace GLMS.Web.Services;

public interface IServiceRequestWorkflowService
{
    bool CanCreateRequest(Contract contract, out string? errorMessage);
}
