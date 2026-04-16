# GLMS Part 2 - Core Prototype & Unit Testing

This Visual Studio solution implements the Part 2 requirements for the Global Logistics Management System (GLMS):

- ASP.NET Core MVC monolith
- SQL Server via Entity Framework Core
- Entities: Client, Contract, ServiceRequest
- PDF signed agreement upload/download for contracts
- Workflow rule: no service requests on Expired or On Hold contracts
- Search/filter contracts by date range and status using LINQ
- External USD → ZAR currency API integration using `HttpClient`
- Auto-calculate local ZAR amount on the Service Request page
- Separate xUnit test project for business logic

## Open in Visual Studio
1. Extract the zip file.
2. Open `GLMS-Part2.sln` in Visual Studio 2022.
3. Restore NuGet packages.
4. Update the SQL Server connection string in `GLMS.Web/appsettings.json` if needed.
5. In Package Manager Console:
   - Set the startup project to **GLMS.Web**
   - Run:
     - `Add-Migration InitialCreate`
     - `Update-Database`
6. Run the application.

## Suggested screenshots for submission
- Home page
- Clients list / create page
- Contracts list with filter results
- Contract create screen with PDF upload
- Service request create screen with live exchange rate + ZAR calculation
- Service request blocked for Expired / On Hold contract
- Test Explorer showing all tests passing

## Notes
- The API service uses `https://api.exchangerate.host/` by default.
- If the live currency API is unavailable, a fallback rate is used so the page still works during marking.
- Uploaded files are saved under `GLMS.Web/wwwroot/uploads/contracts`.

## Projects
- `GLMS.Web` - ASP.NET Core MVC application
- `GLMS.Tests` - xUnit business logic tests
- `Scripts` - starter SQL script
