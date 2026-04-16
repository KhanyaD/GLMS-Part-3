# GLMS Database Migration - Quick Start Guide

## ✅ Migration Files Created Successfully!

Your database migration has been created and includes:

1. ✅ `20250117000000_InitialCreate.cs` - Main migration file
2. ✅ `20250117000000_InitialCreate.Designer.cs` - Designer metadata
3. ✅ `ApplicationDbContextModelSnapshot.cs` - Model snapshot
4. ✅ `InitialCreate_Script.sql` - SQL script for manual execution
5. ✅ `README.md` - Complete documentation

---

## 🚀 Next Steps to Apply Migration

### Option 1: Automatic Migration (Easiest - Recommended)

Your application is already configured to automatically apply migrations when you run it!

**Just follow these steps:**

1. **Stop Debugging** (if running)
   - Press `Shift + F5` in Visual Studio

2. **Run the Application**
   - Press `F5` or click "Start Debugging"
   - The migration will be applied automatically via `Program.cs`

3. **Verify Database Creation**
   - Open **SQL Server Object Explorer** in Visual Studio
   - Navigate to `(localdb)\mssqllocaldb` → Databases → `GLMS_Database`
   - You should see the tables: `Clients`, `Contracts`, `ServiceRequests`

---

### Option 2: Manual Migration via Command Line

If you prefer to apply the migration manually:

```powershell
# 1. Stop debugging first (Shift + F5)

# 2. Open PowerShell or Terminal in Visual Studio

# 3. Navigate to the GLMS.Web folder
cd C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2\GLMS.Web

# 4. Apply the migration
dotnet ef database update

# 5. Verify migration was applied
dotnet ef migrations list
```

---

### Option 3: SQL Script Execution

If Entity Framework tools aren't working:

1. Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**
2. Connect to your local SQL Server instance
3. Open the file: `GLMS.Web\Migrations\InitialCreate_Script.sql`
4. Execute the script
5. Refresh your database to see the new tables

---

## 📋 Database Schema Created

### Tables

| Table | Columns | Description |
|-------|---------|-------------|
| **Clients** | Id, Name, ContactDetails, Region | Stores client information |
| **Contracts** | Id, ClientId, StartDate, EndDate, Status, ServiceLevel, SignedAgreementFileName, SignedAgreementPath | Stores contract details and PDF file references |
| **ServiceRequests** | Id, ContractId, Description, CostUsd, LocalCostZar, ExchangeRateUsed, Status, CreatedAtUtc | Stores service requests with currency conversion |

### Relationships

- `Contracts` → `Clients` (Many-to-One via ClientId)
- `ServiceRequests` → `Contracts` (Many-to-One via ContractId)

Both relationships use **Restrict** delete behavior to prevent accidental data loss.

---

## ✅ Verification Checklist

After applying the migration, verify:

- [ ] Database `GLMS_Database` exists
- [ ] Table `Clients` exists with 4 columns
- [ ] Table `Contracts` exists with 8 columns
- [ ] Table `ServiceRequests` exists with 8 columns
- [ ] Foreign key `FK_Contracts_Clients_ClientId` exists
- [ ] Foreign key `FK_ServiceRequests_Contracts_ContractId` exists
- [ ] Index `IX_Contracts_ClientId` exists
- [ ] Index `IX_ServiceRequests_ContractId` exists

---

## 🎯 For Part 2 Submission

You now have everything required for the database section:

✅ **Database Migration Scripts**: `InitialCreate_Script.sql`  
✅ **EF Core Migrations**: All files in `Migrations` folder  
✅ **Documentation**: `Migrations\README.md`

### Screenshots to Capture:

1. **Migration Files**: Screenshot of the `Migrations` folder showing all files
2. **Database Schema**: Screenshot of SQL Server Object Explorer showing tables
3. **Migration Applied**: Screenshot of terminal showing successful `dotnet ef database update`
4. **Table Structure**: Screenshot of each table's columns and data types

---

## 🔧 Troubleshooting

### Issue: "Build failed"
**Solution:** Stop debugging first (Shift + F5), then try again

### Issue: "Database already exists"
**Solution:** The migration will update the existing database. No action needed.

### Issue: "Cannot connect to database"
**Solution:** Ensure SQL Server LocalDB is running:
```powershell
sqllocaldb start mssqllocaldb
```

### Issue: "Migration already applied"
**Solution:** This is fine! It means the database is up to date.

---

## 📞 Support

If you encounter any issues:

1. Check the `README.md` in the Migrations folder for detailed documentation
2. Verify your connection string in `appsettings.json`
3. Ensure SQL Server LocalDB is installed and running
4. Try the SQL Script method as a fallback

---

**Migration Status:** ✅ Ready to Apply  
**Created:** January 17, 2025  
**Project:** GLMS Part 2 - Core Prototype
