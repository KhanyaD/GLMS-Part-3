using GLMS.Api.Models;

namespace GLMS.Api.Services;

public interface IServiceRequestWorkflowService
{
    bool CanCreateRequest(Contract contract, out string? errorMessage);
}
