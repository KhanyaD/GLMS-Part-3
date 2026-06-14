# Screenshot Verification Script
# Run this after taking all screenshots

Write-Host "=== SCREENSHOT VERIFICATION ===" -ForegroundColor Cyan
Write-Host ""

$screenshotPath = "C:\Users\Kukhanya\GLMS-Part2-VisualStudio-fixed\GLMS-Part2\Screenshots"
$requiredScreenshots = @(
    "TestExplorer_AllPassing.png",
    "Database_Tables.png",
    "Database_Clients.png",
    "Database_Contracts.png",
    "Database_ServiceRequests.png"
)

$optionalScreenshots = @(
    "App_HomePage.png",
    "App_ClientsList.png",
    "App_CreateContract.png",
    "App_ServiceRequest.png"
)

Write-Host "Required Screenshots:" -ForegroundColor Yellow
foreach ($screenshot in $requiredScreenshots) {
    $fullPath = Join-Path $screenshotPath $screenshot
    if (Test-Path $fullPath) {
        Write-Host "[✓] $screenshot" -ForegroundColor Green
    } else {
        Write-Host "[✗] $screenshot - MISSING!" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "Optional Screenshots (Recommended):" -ForegroundColor Yellow
foreach ($screenshot in $optionalScreenshots) {
    $fullPath = Join-Path $screenshotPath $screenshot
    if (Test-Path $fullPath) {
        Write-Host "[✓] $screenshot" -ForegroundColor Green
    } else {
        Write-Host "[○] $screenshot - Not added" -ForegroundColor Gray
    }
}

Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Cyan
Write-Host "1. If all required screenshots are present, run: git add Screenshots/"
Write-Host "2. Then run: git commit -m 'Add submission screenshots'"
Write-Host "3. Finally run: git push origin master"
