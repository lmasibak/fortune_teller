# Daily Fortune Generator Packaging Script
param(
    [string]$Version = "1.0.0",
    [string]$OutputPath = ".\artifacts\packages"
)

Write-Host "📦 Daily Fortune Generator Packaging Script" -ForegroundColor Magenta
Write-Host "=============================================" -ForegroundColor Magenta

$ErrorActionPreference = "Stop"

# Get script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Split-Path -Parent $ScriptDir

# Change to root directory
Set-Location $RootDir

try {
    # Create output directory
    if (!(Test-Path $OutputPath)) {
        New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
    }

    # Build for different platforms
    $Platforms = @(
        @{ Runtime = "win-x64"; Name = "Windows-x64" },
        @{ Runtime = "win-x86"; Name = "Windows-x86" }
    )

    foreach ($Platform in $Platforms) {
        Write-Host "🔨 Building for $($Platform.Name)..." -ForegroundColor Yellow
        
        $PlatformOutput = Join-Path $OutputPath $Platform.Name
        
        dotnet publish "src\DailyFortuneGenerator\DailyFortuneGenerator.csproj" `
            --configuration Release `
            --runtime $Platform.Runtime `
            --self-contained true `
            --output $PlatformOutput `
            -p:PublishSingleFile=true `
            -p:PublishTrimmed=true

        # Create ZIP package
        $ZipPath = Join-Path $OutputPath "DailyFortuneGenerator-$Version-$($Platform.Name).zip"
        Compress-Archive -Path "$PlatformOutput\*" -DestinationPath $ZipPath -Force
        
        Write-Host "✅ Package created: $ZipPath" -ForegroundColor Green
        
        # Clean up platform directory
        Remove-Item $PlatformOutput -Recurse -Force
    }

    Write-Host ""
    Write-Host "🎉 Packaging completed successfully!" -ForegroundColor Green
    Write-Host "Packages created in: $OutputPath" -ForegroundColor Cyan

} catch {
    Write-Host ""
    Write-Host "❌ Packaging failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
