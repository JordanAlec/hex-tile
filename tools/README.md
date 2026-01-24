# HexTile Build Tools

## release.ps1

Single script to build and package a release.

```powershell
.\tools\release.ps1 1.0.0
```

Creates:
- `dist\DesktopApp.exe` - Standalone executable  
- `dist\HexTile-Setup-1.0.0.exe` - Installer

## installer.iss

Inno Setup configuration for the installer.
