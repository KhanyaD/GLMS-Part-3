# 📸 SCREENSHOT SESSION - FOLLOW ALONG

## ✅ SCREENSHOT 1: Test Explorer (All Tests Passing)

### Steps:
1. ✅ Visual Studio is now open
2. Wait for solution to load completely
3. Press **Ctrl + E, T** to open Test Explorer
4. Click the green **"Run All Tests"** button (▶)
5. Wait for all 6 tests to turn GREEN ✅
6. Press **Win + Shift + S** (or use Snipping Tool)
7. Capture the ENTIRE Test Explorer panel
8. Press **Ctrl + V** in Paint or save directly
9. Save as: `Screenshots\TestExplorer_AllPassing.png`

**What should be visible:**
- All 6 test names
- Green checkmarks ✅ on each test
- Test count (6 Passed, 0 Failed)

---

## ✅ SCREENSHOT 2: Database Tables List

### Steps:
1. Press **Ctrl + \, Ctrl + S** (SQL Server Object Explorer)
2. If it doesn't open, go to **View → SQL Server Object Explorer**
3. Expand: **SQL Server**
4. Expand: **(localdb)\MSSQLLocalDB**
5. Expand: **Databases**
6. Expand: **GLMSDb**
7. Click on **Tables** folder to show all tables
8. Press **Win + Shift + S**
9. Capture the Tables folder showing all 3 tables
10. Save as: `Screenshots\Database_Tables.png`

**What should be visible:**
- dbo.Clients
- dbo.Contracts
- dbo.ServiceRequests

---

## ✅ SCREENSHOT 3: Clients Table Data

### Steps:
1. In SQL Server Object Explorer
2. Right-click **dbo.Clients**
3. Click **View Data**
4. Wait for data to load
5. Press **Win + Shift + S**
6. Capture the data grid showing client records
7. Save as: `Screenshots\Database_Clients.png`

**What should be visible:**
- Column headers (Id, Name, ContactDetails, Region)
- At least 2-3 client rows with data

---

## ✅ SCREENSHOT 4: Contracts Table Data

### Steps:
1. Right-click **dbo.Contracts**
2. Click **View Data**
3. Wait for data to load
4. Press **Win + Shift + S**
5. Capture the data grid
6. Save as: `Screenshots\Database_Contracts.png`

**What should be visible:**
- ClientId, StartDate, EndDate, Status columns
- Contract data with foreign keys

---

## ✅ SCREENSHOT 5: Service Requests Table Data

### Steps:
1. Right-click **dbo.ServiceRequests**
2. Click **View Data**
3. Wait for data to load
4. Press **Win + Shift + S**
5. Capture the data grid
6. Save as: `Screenshots\Database_ServiceRequests.png`

**What should be visible:**
- CostUsd, LocalCostZar, ExchangeRateUsed columns
- Service request data showing currency conversions

---

## 🎨 BONUS: Application Screenshots (HIGHLY RECOMMENDED)

### SCREENSHOT 6: Run the Application
1. Press **F5** to run the application
2. Browser will open automatically
3. Wait for home page to load

---

### SCREENSHOT 7: Home Page
1. Make sure you're on the home page
2. Press **Win + Shift + S**
3. Capture the entire browser window
4. Save as: `Screenshots\App_HomePage.png`

---

### SCREENSHOT 8: Clients List (CRUD - Read)
1. Click **Clients** in the navigation menu
2. You should see a list of clients
3. Press **Win + Shift + S**
4. Capture showing the list with Create/Edit/Delete buttons
5. Save as: `Screenshots\App_ClientsList.png`

**This proves your READ functionality! ✅**

---

### SCREENSHOT 9: Create Client Form (CRUD - Create)
1. Click **Create New** button
2. The create form will appear
3. **OPTIONAL:** Fill in a test client:
   - Name: "Test Client"
   - Contact: "test@example.com"
   - Region: "Gauteng"
4. Press **Win + Shift + S**
5. Capture the form (with or without data)
6. Save as: `Screenshots\App_CreateClient.png`

**This proves your CREATE functionality! ✅**

---

### SCREENSHOT 10: Edit Client Form (CRUD - Update) [Optional]
1. Go back to Clients list
2. Click **Edit** on any client
3. Press **Win + Shift + S**
4. Capture the edit form
5. Save as: `Screenshots\App_EditClient.png`

**This proves your UPDATE functionality! ✅**

---

### SCREENSHOT 11: Create Contract with PDF Upload
1. Click **Contracts** in navigation
2. Click **Create New**
3. Fill in contract details
4. Notice the file upload field for PDF
5. Press **Win + Shift + S**
6. Capture the form showing file upload
7. Save as: `Screenshots\App_CreateContract.png`

---

### SCREENSHOT 12: Service Request with Currency Conversion
1. Click **Service Requests** in navigation
2. Click **Create New**
3. Enter a USD amount (e.g., 100)
4. Watch it automatically convert to ZAR!
5. Press **Win + Shift + S**
6. Capture showing the currency conversion
7. Save as: `Screenshots\App_ServiceRequest.png`

**This proves your API integration! ✅**

---

## ✅ VERIFICATION

After taking all screenshots, run:

```powershell
Get-ChildItem Screenshots | Select-Object Name
```

---

## ✅ COMMIT TO GITHUB

```powershell
cd C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2
git add Screenshots/
git commit -m "Add all submission screenshots including CRUD demonstrations"
git push origin master
```

---

## ✅ FINAL CHECK

Open GitHub to verify:
https://github.com/KhanyaD/GLMS-Part2/tree/master/Screenshots

---

## 📝 TIPS

- **Screenshot Tool:** Win + Shift + S (best option)
- **Alternative:** Snipping Tool from Start menu
- **Save Format:** PNG (best quality)
- **File Names:** Must match exactly (case-sensitive)

---

## ⏱️ ESTIMATED TIME

- Required screenshots (1-5): 8 minutes
- Application screenshots (6-12): 10 minutes
- **Total: ~20 minutes**

---

## 🎯 WHAT EACH SCREENSHOT PROVES

✅ Screenshot 1: Unit Testing ✅ Screenshot 2-5: Database Design & Relationships
✅ Screenshot 6-7: Application Running
✅ Screenshot 8: **CRUD - Read (List)**
✅ Screenshot 9: **CRUD - Create**
✅ Screenshot 10: **CRUD - Update** (Optional)
✅ Screenshot 11: File Upload Feature
✅ Screenshot 12: API Integration

---

**KEEP THIS FILE OPEN AS YOU TAKE SCREENSHOTS!**

Check off each one as you complete it! ✅
