# Part 3 Screenshot Checklist

Use this checklist to collect evidence required by the rubric.

## A. Backend API (Swagger/OpenAPI)

- [ ] Run API project and open Swagger UI.
- [ ] Capture Swagger home page showing API title and endpoint list.
- [ ] Capture POST /api/auth/token request and successful response (token visible).
- [ ] Capture GET /api/contracts successful response (200).
- [ ] Capture POST /api/contracts endpoint in Swagger (request body/form shown).
- [ ] Capture PATCH /api/contracts/{id}/status endpoint and successful response.
- [ ] Capture at least one additional endpoint (for example /api/clients or /api/service-requests).

Suggested filename prefix: part3_api_

## B. Frontend MVC Calling API

- [ ] Capture Contracts list page loaded from API.
- [ ] Capture Create Contract form.
- [ ] Capture successful contract creation message.
- [ ] Capture Service Request create page with exchange-rate behavior.
- [ ] Capture successful service request creation message.
- [ ] Capture Clients page CRUD operation (create or edit) showing success.

Suggested filename prefix: part3_mvc_

## C. Automated Integration Testing

- [ ] Open Test Explorer with all tests listed.
- [ ] Run all tests.
- [ ] Capture passing result summary (including integration tests).
- [ ] Capture specific API integration test details (GET /api/contracts assertion).

Suggested filename prefix: part3_tests_

## D. Docker Containerization Evidence

- [ ] Capture terminal with docker compose up --build command.
- [ ] Capture successful container startup logs.
- [ ] Capture Docker Desktop showing all 3 running containers:
  - sql-server-db
  - glms-backend-api
  - glms-frontend-web
- [ ] Capture browser page for frontend running from container (localhost:8080).
- [ ] Capture Swagger/API running from container (localhost:8081/swagger).

Suggested filename prefix: part3_docker_

## E. Submission Evidence Pack

- [ ] Place all screenshots in Screenshots folder.
- [ ] Verify image clarity (text readable).
- [ ] Ensure filenames are descriptive and ordered.
- [ ] Include screenshots in video walkthrough where relevant.

## Recommended Naming Convention

Use this format for easy marking:

part3_<section>_<step>.png

Examples:

- part3_api_01_swagger_home.png
- part3_api_02_get_contracts_ok.png
- part3_tests_01_test_explorer_pass.png
- part3_docker_01_containers_running.png
- part3_mvc_01_contract_create_success.png
