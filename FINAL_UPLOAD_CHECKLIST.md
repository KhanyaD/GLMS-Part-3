# Final Upload Checklist (Part 3)

Use this checklist right before ARC submission.

## 1. Code and Build Verification

- [ ] Solution builds successfully: `dotnet build GLMS-Part2.sln -c Debug`
- [ ] Tests pass successfully: `dotnet test GLMS-Part2.sln -c Debug`
- [ ] API project exists in solution (`GLMS.Api`)
- [ ] MVC project uses API calls via HttpClient
- [ ] Integration tests are included in `GLMS.Tests`

## 2. Required Part 3 Artifacts in Repository

- [ ] `docker-compose.yml` exists at repository root
- [ ] `GLMS.Api/Dockerfile` exists
- [ ] `GLMS.Web/Dockerfile` exists
- [ ] `PART3_TECHNICAL_REFLECTION_REPORT.pdf` exists
- [ ] `PART3_SCREENSHOT_CHECKLIST.pdf` exists
- [ ] `PART3_VIDEO_SCRIPT.pdf` exists
- [ ] `GitHub_Submission_Link.txt` updated for Part 3
- [ ] Video link added in `GitHub_Submission_Link.txt`

## 3. Screenshot Evidence (Must Be Actual Images)

- [ ] Swagger/API evidence screenshots captured
- [ ] MVC frontend evidence screenshots captured
- [ ] Test Explorer/integration test evidence screenshots captured
- [ ] Docker evidence screenshots captured
- [ ] All screenshots placed in `Screenshots` folder as image files (`.png`/`.jpg`)
- [ ] Screenshot filenames follow your Part 3 naming convention
- [ ] Screenshots are clear and readable

## 4. Docker Runtime Evidence

- [ ] `docker compose up --build` executed successfully
- [ ] SQL, API, and Web containers are running together
- [ ] Frontend reachable at `http://localhost:8080`
- [ ] API Swagger reachable at `http://localhost:8081/swagger`
- [ ] Docker Desktop screenshot showing all three running containers captured

## 5. GitHub Submission Readiness

- [ ] All required files committed
- [ ] Changes pushed to GitHub main branch
- [ ] Repository URL opens correctly
- [ ] Lecturer can access repository and files

## 6. ARC Upload Pack (Final)

- [ ] GitHub repository link submitted on ARC
- [ ] Reflection report PDF uploaded
- [ ] Screenshot evidence uploaded
- [ ] Any required scripts/migration files uploaded
- [ ] Video link/file uploaded as required by lecturer

## 7. Final Pre-Submit Sanity Check

- [ ] No placeholders left in docs (e.g., "PASTE YOUR VIDEO LINK HERE")
- [ ] No outdated Part 2 wording in Part 3 submission docs
- [ ] All filenames are clean and descriptive
- [ ] You can explain architecture, testing, and Docker flow in your video

---

## Quick Fail Conditions (Do Not Submit Until Fixed)

- Missing screenshots as individual image files
- Missing demo video or video link
- GitHub link incorrect or private when it should be accessible
- Docker artifacts missing
- Tests/build failing
