# Screenshot Insertion Guide
## For GLMS Final Submission Document

---

## 📸 Where to Insert Each Screenshot

### 1. Test Explorer Screenshot
**Location:** Section 13.1 "Unit Test Summary"  
**File:** `Screenshots/TestExplorer_AllPassing.png`  
**Caption:** "Figure 1: Visual Studio Test Explorer showing all 6 unit tests passing successfully"

**Insert after line:**
```
**Total Tests:** 6  
**Passed:** 6 ✅  
**Failed:** 0  
**Code Coverage:** 85%
```

**Markdown to add:**
```markdown
![Test Explorer - All Tests Passing](Screenshots/TestExplorer_AllPassing.png)

**Figure 1: Visual Studio Test Explorer showing all 6 unit tests passing successfully**

The test suite validates:
- ✅ CurrencyCalculatorTests.ConvertUsdToZar_ReturnsRoundedExpectedValue
- ✅ CurrencyCalculatorTests.ConvertUsdToZar_Throws_WhenRateIsInvalid
- ✅ FileValidationServiceTests.ValidatePdf_AllowsPdfFiles
- ✅ FileValidationServiceTests.ValidatePdf_ThrowsForExeFiles
- ✅ ServiceRequestWorkflowServiceTests.CanCreateRequest_ReturnsTrue_ForActiveContract
- ✅ ServiceRequestWorkflowServiceTests.CanCreateRequest_ReturnsFalse_ForExpiredContract
```

---

### 2. Database Tables Screenshot
**Location:** Section 13.2 "Database Verification"  
**File:** `Screenshots/Database_Tables.png`  
**Caption:** "Figure 2: SQL Server Object Explorer showing all database tables"

**Markdown to add:**
```markdown
#### Database Schema Verification

![Database Tables](Screenshots/Database_Tables.png)

**Figure 2: SQL Server Object Explorer showing the three main tables: Clients, Contracts, and ServiceRequests with proper relationships**
```

---

### 3. Clients Data Screenshot
**Location:** Section 13.2 "Database Verification" - Sample Data subsection  
**File:** `Screenshots/Database_Clients.png`  
**Caption:** "Figure 3: Sample client data demonstrating CRUD functionality"

**Markdown to add:**
```markdown
#### Sample Data - Clients

![Clients Data](Screenshots/Database_Clients.png)

**Figure 3: Sample client records showing Name, ContactDetails, and Region fields populated with test data**
```

---

### 4. Contracts Data Screenshot
**Location:** Section 13.2 "Database Verification" - Sample Data subsection  
**File:** `Screenshots/Database_Contracts.png`  
**Caption:** "Figure 4: Contract records with foreign key relationships to clients"

**Markdown to add:**
```markdown
#### Sample Data - Contracts

![Contracts Data](Screenshots/Database_Contracts.png)

**Figure 4: Contract records demonstrating ClientId foreign key relationships, status values, dates, and file upload paths**
```

---

### 5. Service Requests Data Screenshot
**Location:** Section 13.2 "Database Verification" - Sample Data subsection  
**File:** `Screenshots/Database_ServiceRequests.png`  
**Caption:** "Figure 5: Service request data with currency conversion calculations"

**Markdown to add:**
```markdown
#### Sample Data - Service Requests

![Service Requests Data](Screenshots/Database_ServiceRequests.png)

**Figure 5: Service request records showing USD amounts, ZAR conversions, exchange rates used, and status tracking**
```

---

## 📝 Optional Screenshots (Highly Recommended)

### 6. Application Home Page
**Location:** Section 11 "User Interface Design"  
**File:** `Screenshots/App_HomePage.png`

**Markdown to add:**
```markdown
### 11.4 Application Screenshots

#### Home Page

![Application Home Page](Screenshots/App_HomePage.png)

**Figure 6: GLMS application home page showing responsive Bootstrap design and navigation**
```

---

### 7. Clients List View
**Location:** Section 11 "User Interface Design"  
**File:** `Screenshots/App_ClientsList.png`

**Markdown to add:**
```markdown
#### Clients Management

![Clients List View](Screenshots/App_ClientsList.png)

**Figure 7: Clients list view with Create, Edit, Delete, and Details functionality**
```

---

### 8. Create Contract Form
**Location:** Section 11 "User Interface Design"  
**File:** `Screenshots/App_CreateContract.png`

**Markdown to add:**
```markdown
#### Contract Creation with File Upload

![Create Contract Form](Screenshots/App_CreateContract.png)

**Figure 8: Contract creation form demonstrating file upload validation and client selection dropdown**
```

---

### 9. Service Request with Currency Conversion
**Location:** Section 11 "User Interface Design"  
**File:** `Screenshots/App_ServiceRequest.png`

**Markdown to add:**
```markdown
#### Service Request with Live Currency Conversion

![Service Request Form](Screenshots/App_ServiceRequest.png)

**Figure 9: Service request form showing real-time USD to ZAR conversion using external API**
```

---

## 🔧 How to Insert Screenshots

### Method 1: Using Markdown (Recommended)

1. Open `GLMS_Final_Submission_Document.md` in VS Code or any text editor
2. Find the section mentioned above
3. Add the markdown code at the appropriate location
4. Save the file

### Method 2: Using Word/PDF

If converting to Word or PDF:

1. Open the markdown file in Word (using Pandoc or copy-paste)
2. Place cursor at the location mentioned
3. Insert → Picture → Select the screenshot file
4. Add the caption below the image
5. Apply "Figure" style for consistency

---

## ✅ Verification Checklist

After adding all screenshots, verify:

- [ ] All 5 required screenshots are in the `Screenshots` folder
- [ ] File names match exactly (case-sensitive)
- [ ] Each screenshot has a proper caption
- [ ] Figure numbers are sequential (1-9)
- [ ] Images are clear and readable
- [ ] No sensitive data visible (passwords, real API keys, etc.)

---

## 🚀 Final Steps

After inserting all screenshots:

```powershell
# Verify all screenshots are present
.\Verify-Screenshots.ps1

# Add to git
git add Screenshots/
git add GLMS_Final_Submission_Document.md

# Commit
git commit -m "Add all submission screenshots and update document"

# Push to GitHub
git push origin master
```

---

## 📌 Pro Tips

1. **Screenshot Quality:**
   - Use high resolution (at least 1920x1080)
   - Crop to show only relevant content
   - Ensure text is readable
   - Use PNG format for clarity

2. **Test Explorer Screenshot:**
   - Make sure all tests show green checkmarks ✅
   - Show the full test names
   - Include the test count summary

3. **Database Screenshots:**
   - Show at least 2-3 rows of data
   - Include column headers
   - Show the full table name in SQL Server Object Explorer

4. **Application Screenshots:**
   - Use a clean browser window (no extra tabs)
   - Show the URL in the address bar
   - Demonstrate actual functionality (not empty forms)

---

## 🆘 Troubleshooting

**Q: Screenshots not showing in GitHub?**
A: Make sure the `Screenshots` folder is committed and pushed to the repository.

**Q: Image paths not working?**
A: Use relative paths: `Screenshots/filename.png` (not absolute paths)

**Q: Images too large?**
A: Resize to max 1920px width using Windows Photo app or online tools

**Q: Need to retake a screenshot?**
A: Simply overwrite the existing file with the same name, then commit and push again.

---

Good luck with your submission! 🎓✨
