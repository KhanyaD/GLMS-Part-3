# Database Migration Documentation

## Overview
This folder contains the Entity Framework Core migration files for the Global Logistics Management System (GLMS) database.

## Migration: InitialCreate
**Created:** 2025-01-17  
**Migration ID:** 20250117000000_InitialCreate

### Database Schema

#### Tables Created

1. **Clients**
   - `Id` (int, Primary Key, Identity)
   - `Name` (nvarchar(150), Required)
   - `ContactDetails` (nvarchar(150), Required)
   - `Region` (nvarchar(100), Required)

2. **Contracts**
   - `Id` (int, Primary Key, Identity)
   - `ClientId` (int, Foreign Key to Clients, Required)
   - `StartDate` (datetime2, Required)
   - `EndDate` (datetime2, Required)
   - `Status` (int, Required) - Enum: 0=Draft, 1=Active, 2=Expired, 3=OnHold
   - `ServiceLevel` (nvarchar(100), Required)
   - `SignedAgreementFileName` (nvarchar(255), Optional)
   - `SignedAgreementPath` (nvarchar(500), Optional)

3. **ServiceRequests**
   - `Id` (int, Primary Key, Identity)
   - `ContractId` (int, Foreign Key to Contracts, Required)
   - `Description` (nvarchar(500), Required)
   - `CostUsd` (decimal(18,2), Required)
   - `LocalCostZar` (decimal(18,2), Required)
   - `ExchangeRateUsed` (decimal(18,6), Required)
   - `Status` (int, Required) - Enum: 0=Pending, 1=Approved, 2=Rejected
   - `CreatedAtUtc` (datetime2, Required)

#### Relationships

- **Contracts** → **Clients**: Many-to-One (Delete: Restrict)
- **ServiceRequests** → **Contracts**: Many-to-One (Delete: Restrict)

#### Indexes

- `IX_Contracts_ClientId`: Non-clustered index on Contracts.ClientId
- `IX_ServiceRequests_ContractId`: Non-clustered index on ServiceRequests.ContractId

## How to Apply Migration

### Method 1: Using Entity Framework Core CLI (Recommended)

```bash
# Navigate to the GLMS.Web project folder
cd GLMS.Web

# Apply the migration to the database
dotnet ef database update
```

### Method 2: Automatic Migration on Application Start

The application is configured to automatically apply migrations on startup in `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await SeedData.InitializeAsync(db);
}
```

Simply run the application, and the database will be created/updated automatically.

### Method 3: Using SQL Script

You can also manually run the SQL script:

```bash
# Using SQL Server Management Studio (SSMS)
# Open: InitialCreate_Script.sql
# Execute the script against your database
```

## Connection String

Ensure your `appsettings.json` has the correct connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GLMS_Database;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Migration Files

- `20250117000000_InitialCreate.cs` - Main migration file with Up/Down methods
- `20250117000000_InitialCreate.Designer.cs` - Designer metadata
- `ApplicationDbContextModelSnapshot.cs` - Current model snapshot
- `InitialCreate_Script.sql` - SQL script for manual database creation

## Rollback

To rollback this migration:

```bash
dotnet ef database update 0
```

To remove this migration (if not applied to database):

```bash
dotnet ef migrations remove
```

## Notes for Submission

✅ **Evidence for Part 2:**
- Migration files demonstrate proper Entity Framework Core implementation
- SQL script can be submitted as "Database Migration Scripts"
- Schema matches all requirements:
  - ✅ Client entity (Name, Contact Details, Region)
  - ✅ Contract entity (Linked to Client, StartDate, EndDate, Status, ServiceLevel)
  - ✅ ServiceRequest entity (Linked to Contract, Description, Cost, Status)
  - ✅ File Handling fields (SignedAgreementFileName, SignedAgreementPath)
  - ✅ Proper foreign key relationships with Restrict delete behavior
  - ✅ Appropriate indexes for performance

## Database Schema Diagram

```
┌─────────────┐
│   Clients   │
├─────────────┤
│ Id (PK)     │
│ Name        │
│ Contact     │
│ Region      │
└──────┬──────┘
       │
       │ 1:N
       │
┌──────▼──────────────────┐
│      Contracts          │
├─────────────────────────┤
│ Id (PK)                 │
│ ClientId (FK)           │
│ StartDate               │
│ EndDate                 │
│ Status                  │
│ ServiceLevel            │
│ SignedAgreementFileName │
│ SignedAgreementPath     │
└──────┬──────────────────┘
       │
       │ 1:N
       │
┌──────▼────────────────┐
│  ServiceRequests      │
├───────────────────────┤
│ Id (PK)               │
│ ContractId (FK)       │
│ Description           │
│ CostUsd               │
│ LocalCostZar          │
│ ExchangeRateUsed      │
│ Status                │
│ CreatedAtUtc          │
└───────────────────────┘
```

---
**Generated for:** GLMS Part 2 - The Core Prototype & Unit Testing  
**Date:** January 2025
