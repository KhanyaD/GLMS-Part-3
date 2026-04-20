# 📘 GLMS Part 2 – Core Prototype & Unit Testing

## 📌 Project Overview

The GLMS (Generic Logistics Management System) is a web-based application developed using ASP.NET Core MVC to manage logistics operations efficiently.

This prototype demonstrates core functionality such as managing clients and services, performing CRUD operations, validating user input, and structuring a scalable enterprise-ready application.

The system is designed following the Model-View-Controller (MVC) architectural pattern to ensure separation of concerns and maintainability.

## 🎯 Objectives
- Develop a functional ASP.NET Core MVC application
- Implement full CRUD operations
- Apply server-side validation and error handling
- Demonstrate clean architecture and code structure
- Prepare the application for database and cloud deployment

## 🛠️ Technologies Used

- **Framework:** ASP.NET Core MVC (.NET 8)
- **Language:** C#
- **Database:** Entity Framework Core
- **Database Server:** SQL Server / Azure SQL
- **UI Framework:** Bootstrap (UI Styling)
- **Development Environment:** Visual Studio 2022

## ⚙️ Key Features

- 👤 **Client Management** (Create, Read, Update, Delete)
- 📦 **Service Management**
- 🔍 **Search and filtering functionality**
- ✅ **Input validation** (Data Annotations)
- ⚠️ **Error handling** with user-friendly messages
- 🧪 **Basic unit testing implementation**

## 🧱 Project Structure

```
GLMS-Part2/
│
├── GLMS.Web/              → ASP.NET Core MVC application
│   ├── Controllers/       → Handles application logic
│   ├── Models/            → Defines data structures
│   ├── Views/             → UI (Razor Pages)
│   ├── Services/          → Business logic and API services
│   ├── Data/              → Database context
│   ├── wwwroot/           → Static files (CSS, JS, Images)
│   │   └── uploads/       → Uploaded contract files
│   ├── appsettings.json   → Configuration settings
│   └── Program.cs         → Application entry point
│
├── GLMS.Tests/            → xUnit business logic tests
└── Scripts/               → Starter SQL scripts
```

## 🚀 How to Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/KhanyaD/GLMS-Part2.git
cd GLMS-Part2
```

### 2. Open in Visual Studio 2022

- Open `GLMS-Part2.sln` in Visual Studio 2022

### 3. Restore NuGet Packages

```
Build → Restore NuGet Packages
```

### 4. Configure Database Connection

Update the SQL Server connection string in `appsettings.json` if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GLMS_Database;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 5. Apply Database Migrations

In Package Manager Console:
- Set the startup project to **GLMS.Web**
- Run:
````````
  Add-Migration InitialCreate
  Update-Database
  ````````

Alternatively, the application will automatically apply migrations on startup.

### 6. Run the Application

Press **F5** or click **Start** in Visual Studio

## ⚙️ System Features

- 📄**Client Management:** Add, Edit, Delete, View clients
- 📝 **Contract Management:** Manage contracts with PDF upload support
- 📦 **Service Request Management:** Create and track service requests
- 🔍 **Search andFilter:** Filter contracts by date range and status
- 💱 **Currency Conversion:** Automatic USD to ZAR conversion using live API
- ✅ **Input Validation:** Server-side validation for dataintegrity
- ⚠️ **Business Rules:** Enforce workflow rules (e.g., blocked service requests)
- 🧪 **Unit Testing:** Comprehensive xUnit tests for business logic

## 🧪 Testing

The project includes comprehensive unit testing to validate:

- Data input handling
- Business logic correctness
- Currency calculation accuracy
- Workflow rule enforcement
- Error handling scenarios

Run tests from **Test Explorer** in Visual Studio or use:

```bash
dotnet test
```

## 📸 Suggested Screenshots for Submission

- Application Home Page
- Clients list / create page
- Contracts list with filter results
- Contract create screen with PDF upload
- Service request create screen with live exchange rate + ZAR calculation
- Service request blocked for Expired / On Hold contract
- Database tables (SSMS/Azure)
- Test Explorer showing all tests passing

## ☁️ Deployment 

The application can be deployed using:

- **Azure App Service** for hosting the web application
- **Azure SQL Database** for production database
- **Azure Blob Storage** for file storage (alternative to local uploads)

## 📚 Learning Outcomes

Through this project, the following skills were developed:

- MVC architecture implementation
- Database integration using Entity Framework Core
- RESTful API integration with HttpClient
- File upload and management
- Business rule implementation
- Unit testing with xUnit
- Debugging and troubleshooting
- Web application deployment basics
- Writing structured, maintainable code

## 📌 Important Notes

- The API service uses `https://api.exchangerate.host/` by default for currency conversion
- If the live currency API is unavailable, a fallback rate is used to ensure the application remains functional
- Uploaded contract files are saved under `GLMS.Web/wwwroot/uploads/contracts`
- This is an academic project developed for assessment purposes
- Future improvements may include authentication, role-based access control, enhanced API integration, and improved UI/UX

## 👨‍💻 Author

**Kukhanya Dlanjwa**  
Advanced Diploma in Application Development  
IIE Rosebank College

## 📄 License

This project is developed for educational purposes.



