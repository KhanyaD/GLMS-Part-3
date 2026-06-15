# GLMS Part 3 - Modernizations Docker & Automated Testing

## Project Overview

Global Logistics Management System (GLMS) Part 3 modernises the solution into a service-oriented architecture.

The solution is split into:
- GLMS.Web (ASP.NET Core MVC frontend)
- GLMS.Api (ASP.NET Core Web API backend)
- GLMS.Tests (unit and integration tests)

Part 3 focuses on architectural separation, automated testing, containerization, and CI validation.

## Part 3 Objectives

- Refactor from monolithic flow to MVC + API separation
- Keep business logic and data access in the API layer
- Secure API endpoints with JWT authentication
- Containerize SQL Server, API, and Web with Docker Compose
- Validate build, tests, and Docker in GitHub Actions

## Technology Stack

- .NET 8 (ASP.NET Core)
- C#
- Entity Framework Core
- SQL Server
- xUnit (unit and integration tests)
- Docker and Docker Compose
- GitHub Actions CI/CD

## Solution Structure

```text
GLMS-Part2/
|
|-- GLMS.Api/              # Backend Web API (business logic + data access)
|   |-- Controllers/
|   |-- Data/
|   |-- Services/
|   '-- Program.cs
|
|-- GLMS.Web/              # MVC frontend (calls API via HttpClient)
|   |-- Controllers/
|   |-- Models/
|   |-- Views/
|   |-- Services/
|   '-- Program.cs
|
|-- GLMS.Tests/            # Unit and API integration tests
|-- .github/workflows/     # CI workflow
'-- docker-compose.yml     # SQL + API + Web orchestration
```

## Key Features

- Decoupled frontend and backend
- JWT-protected API endpoints
- Contracts, clients, and service request workflows
- Currency conversion flow support
- Automated unit and integration tests
- Dockerized runtime for full stack
- CI pipeline for restore, build, test, and Docker validation

## Run Locally (Docker)

1. Start Docker Desktop.
2. From solution root, run:

```bash
docker compose up --build -d
```

3. Verify running containers:

```bash
docker compose ps
```

Expected runtime container names:
- glms-part3-sql-server-db-1
- glms-part3-glms-backend-api-1
- glms-part3-glms-frontend-web-1

4. Access services:
- Frontend: http://localhost:8080
- Swagger: http://localhost:8081/swagger

5. Stop containers:

```bash
docker compose down
```

## Run Tests

From solution root:

```bash
dotnet test GLMS-Part2.sln -c Debug
```

## CI/CD

Workflow file:
- .github/workflows/ci-cd.yml

Pipeline validates:
- Restore and build
- Automated test run
- Docker Compose validation
- API and Web image build

## Evidence Checklist (Part 3)

Required evidence files in Screenshots folder:
- 01_Docker_Containers_Running.png
- 02_Docker_Compose_PS.png
- 03_Frontend_Running.png
- 04_Swagger_Running.png
- 05_Tests_Passing.png
- 06_CI_Success.png

## Repository

- GitHub: https://github.com/KhanyaD/GLMS-Part-3

## Author

Kukhanya Dlanjwa
IIE Rosebank College
