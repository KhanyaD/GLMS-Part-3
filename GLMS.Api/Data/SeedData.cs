using GLMS.Api.Models;
using GLMS.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        if (await context.Clients.AnyAsync())
        {
            return;
        }

        var clients = new List<Client>
        {
            new() { Name = "Apex Freight GmbH", ContactDetails = "operations@apexfreight.com", Region = "Europe" },
            new() { Name = "Kalahari Mining Supplies", ContactDetails = "logistics@kalaharims.co.za", Region = "Africa" },
            new() { Name = "BlueWave Imports", ContactDetails = "dispatch@bluewave.com", Region = "North America" }
        };

        context.Clients.AddRange(clients);
        await context.SaveChangesAsync();

        var contracts = new List<Contract>
        {
            new() { ClientId = clients[0].Id, StartDate = DateTime.Today.AddMonths(-2), EndDate = DateTime.Today.AddMonths(8), Status = ContractStatus.Active, ServiceLevel = "Gold" },
            new() { ClientId = clients[1].Id, StartDate = DateTime.Today.AddMonths(-12), EndDate = DateTime.Today.AddDays(-1), Status = ContractStatus.Expired, ServiceLevel = "Silver" },
            new() { ClientId = clients[2].Id, StartDate = DateTime.Today.AddMonths(-1), EndDate = DateTime.Today.AddMonths(5), Status = ContractStatus.OnHold, ServiceLevel = "Platinum" }
        };

        context.Contracts.AddRange(contracts);
        await context.SaveChangesAsync();
    }
}
