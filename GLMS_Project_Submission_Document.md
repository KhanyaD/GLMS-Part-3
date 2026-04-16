# Global Logistics Management System (GLMS) - Project Submission

## Student Information
**Student Name:** [Your Name]  
**Student Number:** [Your Student Number]  
**Module:** Programming 2B / PROG6212  
**Submission Date:** January 2025  
**Project:** Part 2 - The Core Prototype & Unit Testing

---

## Table of Contents
1. [Executive Summary](#executive-summary)
2. [Project Overview](#project-overview)
3. [Requirements Checklist](#requirements-checklist)
4. [Technical Implementation](#technical-implementation)
5. [Database Design](#database-design)
6. [Features Implemented](#features-implemented)
7. [Unit Testing](#unit-testing)
8. [How to Run the Application](#how-to-run-the-application)
9. [GitHub Repository](#github-repository)
10. [Conclusion](#conclusion)

---

## Executive Summary

The Global Logistics Management System (GLMS) is a comprehensive ASP.NET Core MVC application designed to manage international logistics contracts, clients, and service requests. This submission (Part 2) represents the core prototype with full CRUD functionality, database integration, and unit testing.

**Key Deliverables:**
- ✅ Fully functional ASP.NET Core MVC application
- ✅ Entity Framework Core with SQL Server database
- ✅ Complete CRUD operations for Clients, Contracts, and Service Requests
- ✅ Currency conversion integration with external API
- ✅ File upload and validation for contract agreements
- ✅ Unit tests for core business logic
- ✅ Database migrations and seeding

---

## Project Overview

**Application Type:** ASP.NET Core MVC (.NET 8)  
**Database:** SQL Server (LocalDB)  
**Architecture:** Service Layer Pattern with Dependency Injection  
**Testing Framework:** xUnit with Moq

### Project Structure
```
GLMS-Part2/
├── GLMS.Web/                    # Main web application
│   ├── Controllers/             # MVC Controllers
│   ├── Models/                  # Domain entities
│   ├── ViewModels/              # View-specific models
│   ├── Views/                   # Razor views
│   ├── Services/                # Business logic services
│   ├── Data/                    # DbContext and seeding
│   └── Migrations/              # EF Core migrations
└── GLMS.Tests/                  # Unit test project
    └── [Test files]
```

---

## Requirements Checklist

### ✅ Part 2 Requirements Met

| Requirement | Status | Implementation Details |
|------------|--------|------------------------|
| **Database Integration** | ✅ Complete | Entity Framework Core with SQL Server |
| **Three Related Entities** | ✅ Complete | Client, Contract, ServiceRequest |
| **CRUD Operations** | ✅ Complete | All entities have Create, Read, Update, Delete |
| **Foreign Key Relationships** | ✅ Complete | Client→Contract→ServiceRequest |
| **Data Validation** | ✅ Complete | Model validation with DataAnnotations |
| **File Upload Feature** | ✅ Complete | Contract agreement PDF upload |
| **External API Integration** | ✅ Complete | Currency exchange rate API |
| **Unit Testing** | ✅ Complete | xUnit tests for core services |
| **Migrations** | ✅ Complete | EF Core migrations with seed data |
| **Professional UI** | ✅ Complete | Bootstrap-based responsive design |

---

## Technical Implementation

### 1. Database Design

#### Entity Relationship Diagram
```
┌─────────────────┐
│    Clients      │
├─────────────────┤
│ Id (PK)         │
│ Name            │
│ ContactDetails  │
│ Region          │
└────────┬────────┘
         │ 1:N
         │
┌────────▼──────────────────┐
│      Contracts            │
├───────────────────────────┤
│ Id (PK)                   │
│ ClientId (FK)             │
│ StartDate                 │
│ EndDate                   │
│ Status                    │
│ ServiceLevel              │
│ SignedAgreementFileName   │
│ SignedAgreementPath       │
└────────┬──────────────────┘
         │ 1:N
         │
┌────────▼──────────────────┐
│   ServiceRequests         │
├───────────────────────────┤
│ Id (PK)                   │
│ ContractId (FK)           │
│ Description               │
│ CostUsd                   │
│ LocalCostZar              │
│ ExchangeRateUsed          │
│ Status                    │
│ CreatedAtUtc              │
└───────────────────────────┘
```

#### Database Tables

**Clients Table**
- Primary Key: `Id` (int, identity)
- Columns: `Name`, `ContactDetails`, `Region`
- Constraints: All fields required, max lengths enforced

**Contracts Table**
- Primary Key: `Id` (int, identity)
- Foreign Key: `ClientId` → Clients.Id
- Columns: `StartDate`, `EndDate`, `Status`, `ServiceLevel`, `SignedAgreementFileName`, `SignedAgreementPath`
- Constraints: Restrict delete, date validation

**ServiceRequests Table**
- Primary Key: `Id` (int, identity)
- Foreign Key: `ContractId` → Contracts.Id
- Columns: `Description`, `CostUsd`, `LocalCostZar`, `ExchangeRateUsed`, `Status`, `CreatedAtUtc`
- Constraints: Decimal precision for currency, restrict delete

### 2. Service Layer Architecture

**Core Services Implemented:**

1. **ICurrencyCalculator / CurrencyCalculator**
   - Converts USD to ZAR using live exchange rates
   - Caches API responses for performance
   - Error handling for API failures

2. **IFileValidationService / FileValidationService**
   - Validates file types (PDF only)
   - Enforces file size limits (5MB max)
   - Secure file naming and storage

3. **IContractFileService / ContractFileService**
   - Handles file upload and storage
   - Generates unique file names
   - Manages file paths and retrieval

4. **IServiceRequestWorkflowService / ServiceRequestWorkflowService**
   - Business logic for service request approval/rejection
   - Status management (Pending → Approved/Rejected)
   - Workflow validation rules

5. **ICurrencyApiService / CurrencyApiService**
   - External API integration for exchange rates
   - Uses exchangerate.host API
   - Error handling and fallback mechanisms

### 3. Controllers

**ClientsController**
- Index: List all clients with filtering
- Create: Add new client
- Edit: Update client details
- Delete: Remove client (with cascade checks)

**ContractsController**
- Index: List contracts with filtering by status, client, date range
- Create: Create contract with file upload
- Details: View contract with related service requests
- Edit: Update contract information
- Delete: Remove contract

**ServiceRequestsController**
- Index: List service requests with filtering
- Create: Create service request with automatic currency conversion
- Approve/Reject: Workflow actions
- Details: View service request details

---

## Database Design

### Migration Strategy

**Migration Files:**
- `20250117000000_InitialCreate.cs` - Creates all tables and relationships
- `ApplicationDbContextModelSnapshot.cs` - Current schema snapshot

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GLMSDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### Seed Data
The application includes seed data for testing:
- 3 sample clients (Europe, Africa, North America)
- 3 sample contracts (Active, Expired, OnHold statuses)
- Automatic database seeding on first run

---

## Features Implemented

### 1. Client Management
- ✅ Create new clients with validation
- ✅ View client list with search/filter
- ✅ Edit client information
- ✅ Delete clients (with dependency checks)
- ✅ View client's contracts

### 2. Contract Management
- ✅ Create contracts linked to clients
- ✅ Upload signed agreement PDFs
- ✅ File validation (type, size)
- ✅ Contract status management (Draft, Active, Expired, OnHold)
- ✅ Date range validation
- ✅ View contract details with service requests
- ✅ Filter contracts by status, client, date range

### 3. Service Request Management
- ✅ Create service requests linked to contracts
- ✅ Automatic currency conversion (USD → ZAR)
- ✅ Live exchange rate integration
- ✅ Approval/Rejection workflow
- ✅ Status tracking (Pending, Approved, Rejected)
- ✅ Cost calculations with exchange rate history

### 4. Currency Conversion
- ✅ Integration with exchangerate.host API
- ✅ Real-time USD to ZAR conversion
- ✅ Exchange rate storage for audit trail
- ✅ Error handling and fallback rates
- ✅ Configurable API endpoint

### 5. File Management
- ✅ PDF upload for contract agreements
- ✅ File type validation
- ✅ File size limits (5MB)
- ✅ Secure file storage
- ✅ File retrieval and download

---

## Unit Testing

### Test Coverage

**Test Project:** GLMS.Tests (xUnit + Moq)

#### 1. CurrencyCalculatorTests
- ✅ Test successful currency conversion
- ✅ Test API failure handling
- ✅ Test invalid responses
- ✅ Test caching mechanism
- ✅ Test exchange rate precision

#### 2. FileValidationServiceTests
- ✅ Test valid PDF file acceptance
- ✅ Test invalid file type rejection
- ✅ Test file size limit enforcement
- ✅ Test null file handling
- ✅ Test empty file rejection

#### 3. ServiceRequestWorkflowServiceTests
- ✅ Test approval workflow
- ✅ Test rejection workflow
- ✅ Test invalid status transitions
- ✅ Test business rule validation
- ✅ Test concurrent status updates

### Test Execution
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

**Test Results:** All tests passing ✅

---

## How to Run the Application

### Prerequisites
- Visual Studio 2022 (or later)
- .NET 8 SDK
- SQL Server LocalDB

### Steps to Run

1. **Clone the Repository**
   ```bash
   git clone [YOUR-REPOSITORY-URL]
   cd GLMS-Part2
   ```

2. **Open Solution**
   - Open `GLMS-Part2.sln` in Visual Studio

3. **Restore Packages**
   ```bash
   dotnet restore
   ```

4. **Update Database** (Automatic on first run)
   - The application will automatically create the database
   - Migrations will be applied
   - Seed data will be inserted

5. **Run Application**
   - Press F5 in Visual Studio
   - Or: `dotnet run --project GLMS.Web`

6. **Access Application**
   - Navigate to: `https://localhost:[port]`
   - Default landing page shows system overview

### Manual Database Setup (Alternative)

If automatic migration fails:

```bash
# From GLMS.Web directory
dotnet ef database update
```

Or run the SQL script:
- `GLMS.Web/Migrations/InitialCreate_Script.sql`

---

## GitHub Repository

**Repository URL:** [INSERT YOUR GITHUB REPOSITORY URL HERE]

### Repository Structure
```
Repository includes:
✅ Complete source code
✅ Database migrations
✅ Unit tests
✅ Documentation (README.md)
✅ .gitignore configured
✅ Clear commit history
```

### How to Verify Repository Access
1. Open the repository URL in an incognito/private browser window
2. Verify that all files are visible
3. Check that the repository is set to **Public**

### Submission Checklist
- [ ] Repository is set to Public
- [ ] All code is committed and pushed
- [ ] README.md is complete
- [ ] No sensitive data (connection strings, API keys) in repository
- [ ] .gitignore excludes bin/, obj/, and user-specific files

---

## Conclusion

### Summary of Achievements

This GLMS Part 2 submission demonstrates:

1. **Technical Proficiency**
   - Full-stack ASP.NET Core MVC development
   - Entity Framework Core database integration
   - RESTful API consumption
   - Service-oriented architecture

2. **Software Engineering Best Practices**
   - Separation of concerns (MVC + Service Layer)
   - Dependency injection
   - Repository pattern
   - Unit testing with mocking

3. **Business Requirements**
   - Complete client management
   - Contract lifecycle management
   - Service request workflow
   - Currency conversion automation

4. **Code Quality**
   - Clean, readable code
   - Comprehensive validation
   - Error handling
   - Professional UI/UX

### Future Enhancements (Part 3 - WPF)
- Desktop WPF application for offline access
- Advanced reporting and analytics
- Batch operations
- Export to Excel/PDF

---

## Appendices

### A. Code Snippets

**ApplicationDbContext Configuration:**
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Client>()
        .HasMany(c => c.Contracts)
        .WithOne(c => c.Client)
        .HasForeignKey(c => c.ClientId)
        .OnDelete(DeleteBehavior.Restrict);

    // Additional configurations...
}
```

**Currency Conversion Service:**
```csharp
public async Task<decimal> ConvertUsdToZarAsync(decimal amountUsd)
{
    var rate = await _currencyApiService.GetExchangeRateAsync("USD", "ZAR");
    return amountUsd * rate;
}
```

### B. Screenshots

**Recommended Screenshots to Include:**
1. Home page / Dashboard
2. Client list view
3. Contract creation form
4. Contract details with file upload
5. Service request creation with currency conversion
6. Service request approval workflow
7. Database diagram (from SSMS)
8. Unit test results (Test Explorer)

### C. References

- Microsoft Docs: ASP.NET Core MVC
- Entity Framework Core Documentation
- xUnit Testing Framework
- Bootstrap 5 Documentation
- ExchangeRate API Documentation

---

## Declaration

I declare that this is my original work. All sources have been properly acknowledged and referenced. This submission has not been submitted for any other module or qualification.

**Signature:** _______________________  
**Date:** _______________________

---

**End of Submission Document**

---

## Technical Specifications

**Development Environment:**
- IDE: Visual Studio 2022
- Framework: .NET 8
- Language: C# 12
- Database: SQL Server 2022 (LocalDB)

**NuGet Packages:**
- Microsoft.EntityFrameworkCore.SqlServer (8.0.8)
- Microsoft.EntityFrameworkCore.Tools (8.0.8)
- xUnit (2.4.2)
- Moq (4.20.70)
- Bootstrap (5.3.x)

**Browser Compatibility:**
- Chrome (latest)
- Firefox (latest)
- Edge (latest)

---

*This document serves as the official submission for GLMS Part 2. Please ensure all sections are complete before submission.*
