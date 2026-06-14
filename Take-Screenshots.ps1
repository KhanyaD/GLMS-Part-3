# Screenshot Helper Script

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "  GLMS Screenshot Helper" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

$screenshotPath = "Screenshots"

if (!(Test-Path $screenshotPath)) {
    New-Item -Path $screenshotPath -ItemType Directory -Force | Out-Null
    Write-Host "[✓] Created Screenshots folder" -ForegroundColor Green
}

Write-Host "CHECKING REQUIRED SCREENSHOTS:" -ForegroundColor Yellow
Write-Host ""

$required = @(
    "TestExplorer_AllPassing.png",
    "Database_Tables.png",
    "Database_Clients.png",
    "Database_Contracts.png",
    "Database_ServiceRequests.png"
)

foreach ($file in $required) {
    $fullPath = Join-Path $screenshotPath $file
    if (Test-Path $fullPath) {
        Write-Host "[✓] $file" -ForegroundColor Green
    } else {
        Write-Host "[✗] $file - MISSING" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "To take screenshots:" -ForegroundColor Cyan
Write-Host "1. Press Win + Shift + S" -ForegroundColor White
Write-Host "2. Select the area to capture" -ForegroundColor White
Write-Host "3. Open Paint and Ctrl + V" -ForegroundColor White
Write-Host "4. Save to Screenshots folder" -ForegroundColor White
Write-Host ""
