# Deployment Guide

This guide covers how to build, package, and deploy the Daily Fortune Generator application.

## 🏗️ Building the Application

### Prerequisites
- .NET 6.0 SDK or later
- Windows operating system (for WinForms)
- PowerShell 5.1 or later (for build scripts)

### Manual Build

#### Debug Build
\`\`\`bash
dotnet build --configuration Debug
\`\`\`

#### Release Build
\`\`\`bash
dotnet build --configuration Release
\`\`\`

### Automated Build

Use the provided PowerShell build script:

\`\`\`powershell
# Basic build
.\build\build.ps1

# Build with tests
.\build\build.ps1 -Test

# Clean build with packaging
.\build\build.ps1 -Clean -Test -Package

# Specify configuration
.\build\build.ps1 -Configuration Debug -OutputPath ".\custom-output"
\`\`\`

#### Build Script Parameters
- `-Configuration`: Debug or Release (default: Release)
- `-OutputPath`: Output directory (default: .\artifacts)
- `-Clean`: Clean previous builds
- `-Test`: Run unit tests
- `-Package`: Create ZIP package

## 📦 Packaging

### Using Build Script

The build script can create packages automatically:

\`\`\`powershell
.\build\build.ps1 -Package
\`\`\`

### Using Package Script

For advanced packaging with multiple platforms:

\`\`\`powershell
# Package for all supported platforms
.\build\package.ps1

# Package with custom version
.\build\package.ps1 -Version "1.1.0"

# Package to custom location
.\build\package.ps1 -OutputPath ".\releases"
\`\`\`

### Manual Packaging

#### Self-Contained Deployment
\`\`\`bash
# Windows x64
dotnet publish src\DailyFortuneGenerator\DailyFortuneGenerator.csproj \
  --configuration Release \
  --runtime win-x64 \
  --self-contained true \
  --output .\artifacts\win-x64 \
  -p:PublishSingleFile=true \
  -p:PublishTrimmed=true

# Windows x86
dotnet publish src\DailyFortuneGenerator\DailyFortuneGenerator.csproj \
  --configuration Release \
  --runtime win-x86 \
  --self-contained true \
  --output .\artifacts\win-x86 \
  -p:PublishSingleFile=true \
  -p:PublishTrimmed=true
\`\`\`

#### Framework-Dependent Deployment
\`\`\`bash
dotnet publish src\DailyFortuneGenerator\DailyFortuneGenerator.csproj \
  --configuration Release \
  --output .\artifacts\framework-dependent \
  --self-contained false
\`\`\`

## 🚀 Distribution

### ZIP Distribution

1. Build the application using the package script
2. The script creates ZIP files for each platform
3. Distribute the appropriate ZIP file to users
4. Users extract and run the executable

### Installer Creation

#### Using Inno Setup (Recommended)

1. Install [Inno Setup](https://jrsoftware.org/isinfo.php)
2. Create an installer script:

\`\`\`inno
[Setup]
AppName=Daily Fortune Generator
AppVersion=1.0.0
AppPublisher=Fortune Apps Inc.
AppPublisherURL=https://github.com/yourusername/daily-fortune-generator
DefaultDirName={autopf}\Daily Fortune Generator
DefaultGroupName=Daily Fortune Generator
OutputDir=.\artifacts\installer
OutputBaseFilename=DailyFortuneGenerator-Setup-1.0.0
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: ".\artifacts\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Daily Fortune Generator"; Filename: "{app}\DailyFortuneGenerator.exe"
Name: "{group}\{cm:UninstallProgram,Daily Fortune Generator}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\Daily Fortune Generator"; Filename: "{app}\DailyFortuneGenerator.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\DailyFortuneGenerator.exe"; Description: "{cm:LaunchProgram,Daily Fortune Generator}"; Flags: nowait postinstall skipifsilent
\`\`\`

3. Compile the script to create the installer

#### Using WiX Toolset

1. Install [WiX Toolset](https://wixtoolset.org/)
2. Create a WiX project
3. Configure product information and file structure
4. Build the MSI installer

### Microsoft Store Distribution

1. Package as MSIX using Visual Studio
2. Submit to Microsoft Partner Center
3. Follow Microsoft Store certification process

## 🔧 Configuration

### Application Settings

The application uses the following configuration:

- **Default Settings**: Embedded in the application
- **User Preferences**: Stored in application data folder
- **Temporary Files**: Created in system temp directory

### Environment Variables

No environment variables are required for basic operation.

### Registry Settings

The application doesn't modify the Windows registry.

## 🧪 Testing Deployment

### Pre-Deployment Testing

1. **Clean Environment Testing**
   - Test on a machine without development tools
   - Verify .NET runtime requirements
   - Test with different user permissions

2. **Installation Testing**
   - Test installer on different Windows versions
   - Verify uninstallation process
   - Check for leftover files after uninstall

3. **Functionality Testing**
   - Test all application features
   - Verify file save/load operations
   - Test with different user accounts

### Automated Testing

Create automated tests for deployment:

\`\`\`powershell
# Test script example
$TestResults = @()

# Test build
try {
    dotnet build --configuration Release
    $TestResults += "Build: PASS"
} catch {
    $TestResults += "Build: FAIL - $($_.Exception.Message)"
}

# Test publish
try {
    dotnet publish --configuration Release --output .\test-output
    $TestResults += "Publish: PASS"
} catch {
    $TestResults += "Publish: FAIL - $($_.Exception.Message)"
}

# Test executable
try {
    $Process = Start-Process ".\test-output\DailyFortuneGenerator.exe" -PassThru
    Start-Sleep 5
    $Process.CloseMainWindow()
    $TestResults += "Executable: PASS"
} catch {
    $TestResults += "Executable: FAIL - $($_.Exception.Message)"
}

$TestResults | ForEach-Object { Write-Host $_ }
\`\`\`

## 📋 Deployment Checklist

### Pre-Release
- [ ] All tests pass
- [ ] Version numbers updated
- [ ] Documentation updated
- [ ] Changelog updated
- [ ] Build scripts tested
- [ ] Package creation verified

### Release Process
- [ ] Create release build
- [ ] Generate packages for all platforms
- [ ] Test packages on clean systems
- [ ] Create installer (if applicable)
- [ ] Test installer functionality
- [ ] Upload to distribution channels

### Post-Release
- [ ] Verify download links work
- [ ] Monitor for user feedback
- [ ] Document any deployment issues
- [ ] Update deployment procedures if needed

## 🔍 Troubleshooting

### Common Build Issues

**Missing .NET SDK**
- Install .NET 6.0 SDK or later
- Verify installation with `dotnet --version`

**Build Failures**
- Clean solution: `dotnet clean`
- Restore packages: `dotnet restore`
- Check for syntax errors

**Package Creation Issues**
- Verify PowerShell execution policy
- Check file permissions
- Ensure sufficient disk space

### Common Deployment Issues

**Application Won't Start**
- Verify .NET runtime is installed
- Check Windows compatibility
- Review event logs for errors

**Missing Dependencies**
- Use self-contained deployment
- Include all required files
- Test on target system

**Permission Issues**
- Run installer as administrator
- Check antivirus software
- Verify user permissions

## 📞 Support

For deployment issues:
1. Check this documentation first
2. Review GitHub issues for similar problems
3. Create a new issue with detailed information
4. Include system specifications and error messages

## 🔄 Continuous Integration

### GitHub Actions (Example)

\`\`\`yaml
name: Build and Package

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: windows-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 6.0.x
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --no-restore --configuration Release
      
    - name: Test
      run: dotnet test --no-build --configuration Release
      
    - name: Publish
      run: dotnet publish --no-build --configuration Release --output ./artifacts
      
    - name: Upload artifacts
      uses: actions/upload-artifact@v3
      with:
        name: daily-fortune-generator
        path: ./artifacts
\`\`\`

This deployment guide ensures reliable and consistent application distribution across different environments and platforms.
