# Quick Submission Checklist

## 🚀 Ready to Submit? Follow These Steps:

### ☑️ **Step 1: GitHub (30 minutes)**
```powershell
cd C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2
git init
git add .
git commit -m "Part 2: Core Prototype with Database, API Integration, and Unit Tests"
```

1. Create repository on GitHub.com (public)
2. Push code:
```powershell
git remote add origin https://github.com/YOUR_USERNAME/GLMS-Part2.git
git branch -M main
git push -u origin main
```

### ☑️ **Step 2: Run Tests (10 minutes)**

1. Stop debugging: `Shift + F5`
2. Open Test Explorer: `Test → Test Explorer`
3. Click "Run All Tests"
4. **Screenshot:** All 6 tests passing ✅
5. Save as: `Screenshots\TestResults.png`

### ☑️ **Step 3: Database Screenshots (10 minutes)**

1. Apply migration: Run app (F5) or `dotnet ef database update`
2. Open: `View → SQL Server Object Explorer`
3. Navigate to: `(localdb)\mssqllocaldb → Databases → GLMS_Database → Tables`
4. **Screenshot:** Tables list (Clients, Contracts, ServiceRequests)
5. **Screenshot:** Each table's columns
6. Save in `Screenshots` folder

### ☑️ **Step 4: Record Video (1-2 hours)**

**What to show:**
1. **Code walkthrough** (5 min)
   - Models, Services, Controllers
   - Database Context
   - Unit Tests

2. **Running app** (10 min)
   - Create Client
   - Create Contract + Upload PDF
   - Filter Contracts by date/status
   - Create Service Request (show auto-calculation)
   - Try to create request on Expired contract (show error)
   - Download PDF

3. **Tests** (3 min)
   - Show Test Explorer
   - Run tests live

4. **Database** (2 min)
   - Show tables in SQL Server Object Explorer

**Tools:** OBS Studio, Windows Game Bar, or Zoom

### ☑️ **Step 5: Submit on ARC (15 minutes)**

**Upload:**
1. ZIP folder with:
   - Screenshots
   - Database script: `GLMS.Web\Migrations\InitialCreate_Script.sql`
   - README with GitHub link
   - Video (or YouTube link)

2. **In submission box, write:**
```
GitHub: https://github.com/YOUR_USERNAME/GLMS-Part2
Video: [Link or attached]

All Part 2 requirements completed:
✅ Database (EF Core, SQL Server)
✅ Models (Client, Contract, ServiceRequest)
✅ File Upload/Download (PDF)
✅ Workflow Logic (Status validation)
✅ Search/Filter (LINQ)
✅ External API (Currency Exchange)
✅ Auto-calculation (USD to ZAR)
✅ Unit Tests (6 tests passing)
```

---

## 📁 **Files You Already Have:**

✅ `GLMS.Web\Migrations\InitialCreate_Script.sql` - Database script  
✅ `GLMS.Web\Migrations\README.md` - Migration docs  
✅ `MIGRATION_GUIDE.md` - How to apply migration  
✅ `SUBMISSION_CHECKLIST.md` - This guide (detailed)  

---

## ⚡ **Quick Commands:**

**Test:**
```powershell
dotnet test
```

**Migration:**
```powershell
cd GLMS.Web
dotnet ef database update
```

**Run App:**
```
F5 in Visual Studio
```

---

## ⏰ **Time Estimate:**

- GitHub setup: 30 min
- Screenshots: 20 min
- Video recording: 1-2 hours
- Package & submit: 15 min

**Total: ~2-3 hours**

---

## 🎯 **What Makes Your Submission Complete:**

1. ✅ GitHub link (5% penalty if missing!)
2. ✅ All code in repository
3. ✅ Database migration script
4. ✅ Test screenshots (6 tests passing)
5. ✅ Video demonstration
6. ✅ All features working

---

## 📞 **Stuck?**

See `SUBMISSION_CHECKLIST.md` for detailed instructions!

---

**You're almost done! Just follow the 5 steps above and you're ready to submit! 🚀**
