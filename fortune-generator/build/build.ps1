# Daily Fortune Generator Build Script
param(
    [string]$Configuration = "Release",
    [string]$OutputPath = ".\artifacts",
    [switch]$Clean,
    [switch]$Test,
    [switch]$Package
)

Write-Host "🔮 Daily Fortune Generator Build Script" -ForegroundColor Magenta
Write-Host "=======================================" -ForegroundColor Magenta

# Set error action preference
$ErrorActionPreference = "Stop"

# Get script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Split-Path -Parent $ScriptDir

# Change to root directory
Set-Location $RootDir

try {
    # Clean if requested
    if ($Clean) {
        Write-Host "🧹 Cleaning previous builds..." -ForegroundColor Yellow
        if (Test-Path $OutputPath) {
            Remove-Item $OutputPath -Recurse -Force
        }
        dotnet clean --configuration $Configuration
        Write-Host "✅ Clean completed" -ForegroundColor Green
    }

    # Create output directory
    if (!(Test-Path $OutputPath)) {
        New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
    }

    # Restore packages
    Write-Host "📦 Restoring NuGet packages..." -ForegroundColor Yellow
    dotnet restore
    Write-Host "✅ Package restore completed" -ForegroundColor Green

    # Build solution
    Write-Host "🔨 Building solution..." -ForegroundColor Yellow
    dotnet build --configuration $Configuration --no-restore
    Write-Host "✅ Build completed" -ForegroundColor Green

    # Run tests if requested
    if ($Test) {
        Write-Host "🧪 Running tests..." -ForegroundColor Yellow
        dotnet test --configuration $Configuration --no-build --verbosity normal
        Write-Host "✅ Tests completed" -ForegroundColor Green
    }

    # Publish application
    Write-Host "📋 Publishing application..." -ForegroundColor Yellow
    $PublishPath = Join-Path $OutputPath "publish"
    dotnet publish "src\DailyFortuneGenerator\DailyFortuneGenerator.csproj" `
        --configuration $Configuration `
        --output $PublishPath `
        --no-build `
        --self-contained false
    Write-Host "✅ Publish completed" -ForegroundColor Green

    # Package if requested
    if ($Package) {
        Write-Host "📦 Creating package..." -ForegroundColor Yellow
        $PackagePath = Join-Path $OutputPath "DailyFortuneGenerator-v1.0.0.zip"
        Compress-Archive -Path "$PublishPath\*" -DestinationPath $PackagePath -Force
        Write-Host "✅ Package created: $PackagePath" -ForegroundColor Green
    }

    Write-Host ""
    Write-Host "🎉 Build completed successfully!" -ForegroundColor Green
    Write-Host "Output directory: $OutputPath" -ForegroundColor Cyan

} catch {
    Write-Host ""
    Write-Host "❌ Build failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
