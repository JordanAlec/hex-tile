#Requires -Version 5.1

<#
.SYNOPSIS
    Build and package HexTile

.EXAMPLE
    .\tools\release.ps1 1.0.0
#>

param(
    [Parameter(Mandatory=$true)]
    [string]$Version
)

$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $PSScriptRoot
$project = Join-Path $root "src\DesktopApp\DesktopApp.csproj"
$dist = Join-Path $root "dist"
$issFile = Join-Path $root "tools\installer.iss"

Write-Host "`nBuilding HexTile $Version...`n" -ForegroundColor Cyan

# Clean
if (Test-Path $dist) { Remove-Item $dist -Recurse -Force }

# Build (without single-file - WPF doesn't work well with it)
& dotnet publish $project `
    --configuration Release `
    --runtime win-x64 `
    --self-contained true `
    -p:Version=$Version `
    --output $dist `
    --nologo

if ($LASTEXITCODE -ne 0) { exit 1 }

# Rename DesktopApp.exe to HexTile.exe
$oldExe = Join-Path $dist "DesktopApp.exe"
$newExe = Join-Path $dist "HexTile.exe"
if (Test-Path $oldExe) {
    Move-Item $oldExe $newExe -Force
    
    # Also rename the PDB if it exists
    $oldPdb = Join-Path $dist "DesktopApp.pdb"
    $newPdb = Join-Path $dist "HexTile.pdb"
    if (Test-Path $oldPdb) {
        Move-Item $oldPdb $newPdb -Force
    }
}

# Create installer
$iscc = "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"
& $iscc /DMyAppVersion=$Version $issFile | Out-Null

if ($LASTEXITCODE -ne 0) { exit 1 }

# Show results
Write-Host "`nBuild complete!`n" -ForegroundColor Green
$installer = Get-ChildItem $dist -Filter "HexTile-Setup-$Version.exe"
if ($installer) {
    $size = [math]::Round($installer.Length / 1MB, 1)
    Write-Host "  Installer: HexTile-Setup-$Version.exe - $size MB" -ForegroundColor White
    Write-Host "  Location:  $($installer.FullName)" -ForegroundColor Gray
}
Write-Host ""

