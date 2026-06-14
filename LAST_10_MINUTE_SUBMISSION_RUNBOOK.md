# Last 10-Minute Submission Runbook (Part 3)

Follow these steps in order. Do not skip.

## Minute 0 to 2: Final Tech Health Check

1. Build solution:
   dotnet build GLMS-Part2.sln -c Debug
2. Run tests:
   dotnet test GLMS-Part2.sln -c Debug
3. If any failure appears, stop and fix before submission.

## Minute 2 to 4: Docker Proof Check

1. Run stack:
   docker compose up --build
2. Confirm all services are running:
   - sql-server-db
   - glms-backend-api
   - glms-frontend-web
3. Confirm URLs open:
   - Frontend: http://localhost:8080
   - Swagger: http://localhost:8081/swagger
4. Capture missing Docker screenshots immediately.

## Minute 4 to 6: Evidence Files Check

1. Open Screenshots folder.
2. Confirm required images are present as PNG/JPG (not only DOCX/PDF).
3. Confirm naming pattern:
   part3_<section>_<step>.png
4. Confirm image clarity and readable text.

## Minute 6 to 7: Documents Check

1. Confirm these files exist:
   - PART3_TECHNICAL_REFLECTION_REPORT.pdf
   - PART3_SCREENSHOT_CHECKLIST.pdf
   - PART3_VIDEO_SCRIPT.pdf
2. Confirm GitHub_Submission_Link.txt is updated for Part 3.
3. Confirm your video link is pasted in GitHub_Submission_Link.txt.

## Minute 7 to 8: GitHub Check

1. Verify all final files are committed.
2. Push latest commit to GitHub main.
3. Open your repository URL in browser and verify files are visible.

## Minute 8 to 10: ARC Upload

1. Submit GitHub repository link.
2. Upload required PDFs.
3. Upload screenshot evidence.
4. Upload or paste video link as instructed.
5. Review submission summary before final submit.

## Hard Stop Rules (Do Not Submit If Any Are True)

- Build or tests fail.
- Screenshots are missing or unreadable.
- Video link is missing.
- GitHub link is wrong or not updated.
- Docker evidence not captured.

## Quick Recovery If You Are Late

1. Submit correct GitHub link first.
2. Submit reflection PDF and screenshot checklist PDF.
3. Submit all screenshot evidence files.
4. Submit video link.
5. Add any remaining supporting files immediately after, if ARC allows resubmission.
