# 📸 SIMPLE SCREENSHOT INSTRUCTIONS

## What You Need To Do Right Now:

### 1. Open Visual Studio (if not already open)
- Your solution should be open at: `GLMS-Part2.sln`

### 2. Take Test Explorer Screenshot (2 minutes)
Steps:
1. In Visual Studio, press **Ctrl + E, T**
2. Click **"Run All Tests"** button (▶ icon)
3. Wait for all 6 tests to show green checkmarks ✅
4. Press **Win + Shift + S**
5. Click and drag to select the Test Explorer window
6. Open **Paint** app
7. Press **Ctrl + V**
8. Click **File → Save As**
9. Navigate to: `C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2\Screenshots`
10. Save as: `TestExplorer_AllPassing.png`

### 3. Take Database Screenshots (5 minutes)
Steps:
1. In Visual Studio, press **Ctrl + \, Ctrl + S**
2. Expand: **SQL Server → (localdb)\MSSQLLocalDB → Databases → GLMSDb → Tables**

**Screenshot 1: Tables List**
- Press **Win + Shift + S** and capture the Tables folder
- Save as: `Screenshots\Database_Tables.png`

**Screenshot 2: Clients Data**
- Right-click **dbo.Clients** → **View Data**
- Press **Win + Shift + S** and capture the data grid
- Save as: `Screenshots\Database_Clients.png`

**Screenshot 3: Contracts Data**
- Right-click **dbo.Contracts** → **View Data**
- Press **Win + Shift + S** and capture
- Save as: `Screenshots\Database_Contracts.png`

**Screenshot 4: Service Requests Data**
- Right-click **dbo.ServiceRequests** → **View Data**
- Press **Win + Shift + S** and capture
- Save as: `Screenshots\Database_ServiceRequests.png`

### 4. Verify Screenshots
Run this command:
```powershell
Get-ChildItem Screenshots | Select-Object Name
```

You should see:
- TestExplorer_AllPassing.png
- Database_Tables.png
- Database_Clients.png
- Database_Contracts.png
- Database_ServiceRequests.png

### 5. Commit to GitHub
```powershell
git add Screenshots/
git commit -m "Add final submission screenshots"
git push origin master
```

### 6. Verify on GitHub
Open: https://github.com/KhanyaD/GLMS-Part2/tree/master/Screenshots

---

## ✅ DONE!

Once you complete these steps, your submission is 100% ready!

## Need Help?
- Can't find Test Explorer? Press **Ctrl + E, T**
- Can't find SQL Server Object Explorer? Press **Ctrl + \, Ctrl + S**
- Win + Shift + S not working? Use **Snipping Tool** from Start menu
- Screenshots folder doesn't exist? Create it manually in your project folder

---

**Total Time: ~10 minutes** ⏱️
