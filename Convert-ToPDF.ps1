# Convert Markdown to PDF using Microsoft Word
# This script converts GLMS_Final_Submission_Document.md to PDF

Write-Host "Converting GLMS Submission Document to PDF..." -ForegroundColor Cyan
Write-Host ""

$markdownFile = "$PWD\GLMS_Final_Submission_Document.md"
$wordFile = "$PWD\GLMS_Submission_Kukhanya_Dlanjwa.docx"
$pdfFile = "$PWD\GLMS_Submission_Kukhanya_Dlanjwa.pdf"

if (!(Test-Path $markdownFile)) {
    Write-Host "[✗] Markdown file not found: $markdownFile" -ForegroundColor Red
    exit
}

try {
    Write-Host "[1/3] Reading markdown content..." -ForegroundColor Yellow
    $content = Get-Content $markdownFile -Raw

    Write-Host "[2/3] Creating Word document..." -ForegroundColor Yellow

    # Create Word Application
    $word = New-Object -ComObject Word.Application
    $word.Visible = $false

    # Create new document
    $doc = $word.Documents.Add()

    # Insert markdown content
    $selection = $word.Selection
    $selection.TypeText($content)

    # Format the document
    $doc.PageSetup.TopMargin = 72    # 1 inch
    $doc.PageSetup.BottomMargin = 72
    $doc.PageSetup.LeftMargin = 72
    $doc.PageSetup.RightMargin = 72

    # Save as DOCX first
    $doc.SaveAs([ref]$wordFile)
    Write-Host "[✓] Word document created: GLMS_Submission_Kukhanya_Dlanjwa.docx" -ForegroundColor Green

    Write-Host "[3/3] Converting to PDF..." -ForegroundColor Yellow

    # Save as PDF
    $doc.SaveAs([ref]$pdfFile, [ref]17) # 17 = wdFormatPDF

    # Close and cleanup
    $doc.Close()
    $word.Quit()
    [System.Runtime.Interopservices.Marshal]::ReleaseComObject($word) | Out-Null

    Write-Host ""
    Write-Host "[✓] PDF created successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "📄 Output files:" -ForegroundColor Cyan
    Write-Host "   Word: $wordFile" -ForegroundColor White
    Write-Host "   PDF:  $pdfFile" -ForegroundColor White
    Write-Host ""
    Write-Host "Opening PDF..." -ForegroundColor Yellow
    Start-Process $pdfFile

} catch {
    Write-Host "[✗] Error during conversion: $_" -ForegroundColor Red
    Write-Host ""
    Write-Host "Alternative methods:" -ForegroundColor Yellow
    Write-Host "1. Install Pandoc: winget install pandoc" -ForegroundColor White
    Write-Host "2. Use online converter: https://www.markdowntopdf.com/" -ForegroundColor White
    Write-Host "3. Copy content to Word manually and save as PDF" -ForegroundColor White
}
