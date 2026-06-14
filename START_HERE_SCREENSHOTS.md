# ✅ SIMPLE 3-SCREENSHOT CHECKLIST

## 🎯 YOU NEED EXACTLY 3 SCREENSHOTS

---

## ▶️ STEP 1: START YOUR APP

**In Visual Studio:**
1. Press **F5** (or click the green ▶️ "Start" button)
2. Wait for browser to open
3. You'll see the home page

---

## 📸 SCREENSHOT #1: Clients List (MOST IMPORTANT!)

### What to do:
1. In the browser, click **"Clients"** in the navigation menu
2. OR type in address bar: `https://localhost:5001/Clients`
3. Press Enter

### What you should see:
✅ Title: "Clients" with icon
✅ Blue button: "Create New Client"
✅ Table with columns: Name | Contact Details | Region | Actions
✅ Each row has 3 buttons: Details (blue), Edit (yellow), Delete (red)
✅ At least 2-3 client rows

### If table is empty:
- Click "Create New Client"
- Add 2-3 test clients first (see bottom of this file for sample data)

### Take the screenshot:
1. Press **Win + Shift + S**
2. Click and drag to select the ENTIRE page
3. Open **Paint** (from Start menu)
4. Press **Ctrl + V**
5. Click **File → Save As**
6. Navigate to: `C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2\Screenshots`
7. Filename: `Screenshot1_ClientsIndex.png`
8. Click **Save**

✅ **Screenshot #1 DONE!**

---

## 📸 SCREENSHOT #2: Create Client Form

### What to do:
1. Click the **"Create New Client"** button
2. You'll see a form with empty fields

### What you should see:
✅ Title: "Create New Client"
✅ Form fields:
   - Client Name (empty)
   - Contact Details (empty)
   - Region (empty)
✅ Two buttons at bottom:
   - "Back to List" (gray)
   - "Create Client" (blue)

### Take the screenshot:
1. Press **Win + Shift + S**
2. Select the entire form
3. Open **Paint** → **Ctrl + V**
4. Save as: `Screenshots\Screenshot2_CreateClient.png`

✅ **Screenshot #2 DONE!**

---

## 📸 SCREENSHOT #3: Edit Client Form

### What to do:
1. Click **"Back to List"** to go back to Clients page
2. Click **"Edit"** button on ANY client row (yellow button)
3. You'll see a form WITH DATA already filled in

### What you should see:
✅ Title: "Edit Client"
✅ Form fields FILLED with existing data:
   - Client Name (has text)
   - Contact Details (has text)
   - Region (has text)
✅ Two buttons at bottom:
   - "Back to List" (gray)
   - "Save Changes" (yellow)

### Take the screenshot:
1. Press **Win + Shift + S**
2. Select the entire form
3. Open **Paint** → **Ctrl + V**
4. Save as: `Screenshots\Screenshot3_EditClient.png`

✅ **Screenshot #3 DONE!**

---

## ✅ VERIFY YOUR SCREENSHOTS

Run this in PowerShell:
```powershell
Get-ChildItem Screenshots\Screenshot*.png | Select-Object Name, Length
```

You should see:
- Screenshot1_ClientsIndex.png
- Screenshot2_CreateClient.png
- Screenshot3_EditClient.png

---

## 🚀 COMMIT TO GITHUB

```powershell
git add Screenshots/
git add GLMS.Web/
git commit -m "Add Client CRUD screenshots and complete functionality"
git push origin master
```

---

## 📝 SAMPLE CLIENT DATA (If You Need To Create Clients)

**Client 1:**
- Client Name: `Acme Logistics Ltd`
- Contact Details: `john@acmelogistics.com | +27 11 123 4567`
- Region: `Johannesburg, South Africa`

**Client 2:**
- Client Name: `Global Freight Solutions`
- Contact Details: `info@globalfreight.co.za | +27 21 987 6543`
- Region: `Cape Town, South Africa`

**Client 3:**
- Client Name: `Express Cargo Services`
- Contact Details: `contact@expresscargo.com | +27 31 555 7890`
- Region: `Durban, South Africa`

---

## 🎉 YOU'RE DONE!

Once you have all 3 screenshots:
1. Close the application (Shift + F5 in Visual Studio)
2. Commit and push to GitHub (commands above)
3. Move on to the next part of your submission!

---

⏱️ **Total time: 10 minutes**

🎯 **Now press F5 in Visual Studio and start taking screenshots!**
