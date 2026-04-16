using GLMS.Web.Models;
using GLMS.Web.Models.Enums;
using GLMS.Web.Services;
using Xunit;

namespace GLMS.Tests;

public class ServiceRequestWorkflowServiceTests
{
    [Fact]
    public void CanCreateRequest_ReturnsFalse_ForExpiredContract()
    {
        var service = new ServiceRequestWorkflowService();
        var contract = new Contract
        {
            Status = ContractStatus.Expired,
            StartDate = DateTime.Today.AddMonths(-6),
            EndDate = DateTime.Today.AddDays(-1)
        };

        var result = service.CanCreateRequest(contract, out var error);

        Assert.False(result);
        Assert.False(string.IsNullOrWhiteSpace(error));
    }

    [Fact]
    public void CanCreateRequest_ReturnsTrue_ForActiveContract()
    {
        var service = new ServiceRequestWorkflowService();
        var contract = new Contract
        {
            Status = ContractStatus.Active,
            StartDate = DateTime.Today.AddDays(-7),
            EndDate = DateTime.Today.AddMonths(1)
        };

        var result = service.CanCreateRequest(contract, out var error);

        Assert.True(result);
        Assert.Null(error);
    }
}
