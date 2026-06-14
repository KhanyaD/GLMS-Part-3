# 📸 SCREENSHOT COMPLETION CHECKLIST

## ⚡ QUICK START - 10 Minutes to Complete

### Option 1: Automated Helper (Recommended)
```powershell
cd C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2
.\Take-Screenshots.ps1
```

### Option 2: Manual Steps

---

## 🎯 REQUIRED SCREENSHOTS (5 total)

### 1️⃣ Test Explorer - All Tests Passing
**Time:** 2 minutes

```powershell
# In Visual Studio:
# 1. Press Ctrl + E, T (Test Explorer)
# 2. Click "Run All Tests"
# 3. Wait for green checkmarks
# 4. Press Win + Shift + S
# 5. Save as: Screenshots\TestExplorer_AllPassing.png
```

**What to capture:**
- [x] All 6 tests visible
- [x] Green checkmarks on all tests
- [x] Test names clearly readable
- [x] Test count summary visible

---

### 2️⃣ Database Tables List
**Time:** 1 minute

```powershell
# In Visual Studio:
# 1. Press Ctrl + \, Ctrl + S (SQL Server Object Explorer)
# 2. Expand: (localdb)\MSSQLLocalDB → Databases → GLMSDb → Tables
# 3. Press Win + Shift + S
# 4. Save as: Screenshots\Database_Tables.png
```

**What to capture:**
- [x] dbo.Clients table
- [x] dbo.Contracts table
- [x] dbo.ServiceRequests table
- [x] Folder hierarchy visible

---

### 3️⃣ Clients Table Data
**Time:** 1 minute

```powershell
# In SQL Server Object Explorer:
# 1. Right-click dbo.Clients
# 2. Click "View Data"
# 3. Press Win + Shift + S
# 4. Save as: Screenshots\Database_Clients.png
```

**What to capture:**
- [x] At least 2-3 client records
- [x] All columns visible (Id, Name, ContactDetails, Region)
- [x] Table name visible in tab

---

### 4️⃣ Contracts Table Data
**Time:** 1 minute

```powershell
# In SQL Server Object Explorer:
# 1. Right-click dbo.Contracts
# 2. Click "View Data"
# 3. Press Win + Shift + S
# 4. Save as: Screenshots\Database_Contracts.png
```

**What to capture:**
- [x] At least 2-3 contract records
- [x] ClientId, StartDate, EndDate, Status visible
- [x] File paths visible (if any)

---

### 5️⃣ Service Requests Table Data
**Time:** 1 minute

```powershell
# In SQL Server Object Explorer:
# 1. Right-click dbo.ServiceRequests
# 2. Click "View Data"
# 3. Press Win + Shift + S
# 4. Save as: Screenshots\Database_ServiceRequests.png
```

**What to capture:**
- [x] At least 2-3 service request records
- [x] CostUsd, LocalCostZar, ExchangeRateUsed visible
- [x] ContractId foreign key visible

---

## 🎨 OPTIONAL SCREENSHOTS (Recommended for bonus marks)

### 6️⃣ Application Home Page
**Time:** 1 minute

```powershell
# Press F5 to run application
# Take screenshot of home page
# Save as: Screenshots\App_HomePage.png
```

---

### 7️⃣ Clients List View
**Time:** 1 minute

```powershell
# Navigate to Clients
# Take screenshot
# Save as: Screenshots\App_ClientsList.png
```

---

### 8️⃣ Create Contract Form
**Time:** 1 minute

```powershell
# Navigate to Contracts → Create
# Take screenshot
# Save as: Screenshots\App_CreateContract.png
```

---

### 9️⃣ Service Request with Currency
**Time:** 1 minute

```powershell
# Navigate to Service Requests → Create
# Enter USD amount to see conversion
# Take screenshot
# Save as: Screenshots\App_ServiceRequest.png
```

---

## ✅ VERIFICATION

After taking all screenshots, run:

```powershell
.\Verify-Screenshots.ps1
```

This will show you which screenshots are present and which are missing.

---

## 🚀 FINAL COMMIT

Once all screenshots are taken:

```powershell
# Add screenshots to git
git add Screenshots/

# Commit
git commit -m "Add all submission screenshots"

# Push to GitHub
git push origin master

# Verify online
start https://github.com/KhanyaD/GLMS-Part2
```

---

## 📋 COMPLETION CHECKLIST

Mark off as you complete:

**Required:**
- [ ] TestExplorer_AllPassing.png
- [ ] Database_Tables.png
- [ ] Database_Clients.png
- [ ] Database_Contracts.png
- [ ] Database_ServiceRequests.png

**Optional (Recommended):**
- [ ] App_HomePage.png
- [ ] App_ClientsList.png
- [ ] App_CreateContract.png
- [ ] App_ServiceRequest.png

**Final Steps:**
- [ ] All screenshots verified with Verify-Screenshots.ps1
- [ ] Screenshots committed to git
- [ ] Screenshots pushed to GitHub
- [ ] Screenshots visible on GitHub repo
- [ ] Document updated with screenshot references

---

## 💡 TIPS

1. **Window Snip Tool:** Use Win + Shift + S for quick screenshots
2. **Quality:** Make sure text is readable (zoom in if needed)
3. **Cropping:** Crop to show only relevant content
4. **File Names:** Match file names exactly (case-sensitive)
5. **Resolution:** Use high resolution (1920x1080 or higher)

---

## ⏱️ TOTAL TIME ESTIMATE

- Required screenshots: ~10 minutes
- Optional screenshots: ~5 minutes
- Verification & commit: ~2 minutes
- **Total: 15-20 minutes**

---

## 🆘 NEED HELP?

If screenshots aren't working:
1. Make sure Visual Studio is running
2. Make sure the application has data (run it first)
3. Check that Screenshots folder exists
4. Verify file permissions

---

**You're almost done! Just take these screenshots and your submission is 100% complete!** 🎉
