# ╔═══════════════════════════════════════════════════════════════╗
# ║  J4CKASS MILSIM - Deploy Script v1.0 (PowerShell)              ║
# ║  Deployment automático para Windows Server                     ║
# ╚═══════════════════════════════════════════════════════════════╝

param(
    [string]$AppDir = "C:\j4ckass",
    [string]$BackupDir = "C:\Backups\j4ckass"
)

# Requires admin
if (-not ([Security.Principal.WindowsIdentity]::GetCurrent().Groups -contains 'S-1-5-32-544')) {
    Write-Error "Este script requer privilégios de administrador"
    exit 1
}

# Colors
function Write-Header { Write-Host "`n" -ForegroundColor Blue; Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Blue; Write-Host $args[0] -ForegroundColor Blue; Write-Host "═══════════════════════════════════════════════════════════════`n" -ForegroundColor Blue }
function Write-Success { Write-Host "✓ $($args[0])" -ForegroundColor Green }
function Write-Error { Write-Host "✗ $($args[0])" -ForegroundColor Red; exit 1 }
function Write-Warning { Write-Host "⚠ $($args[0])" -ForegroundColor Yellow }

# Check .NET
Write-Header "Verificando Dependências"

try {
    $dotnetVersion = dotnet --version
    Write-Success ".NET $dotnetVersion instalado"
} catch {
    Write-Error ".NET Runtime não encontrado. Instale .NET 9.0 Runtime."
}

# Check environment variables
if (-not $env:ADMIN_EMAIL -or -not $env:ADMIN_PASSWORD) {
    Write-Warning "Variáveis de ambiente não definidas:"
    Write-Host "  ADMIN_EMAIL=$($env:ADMIN_EMAIL)" -ForegroundColor Yellow
    Write-Host "  ADMIN_PASSWORD=$($env:ADMIN_PASSWORD)" -ForegroundColor Yellow
    $response = Read-Host "Continuar mesmo assim? (s/n)"
    if ($response -ne 's' -and $response -ne 'S') {
        exit 1
    }
}

# Backup
Write-Header "Backup do Banco de Dados"

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
if (-not (Test-Path $BackupDir)) {
    New-Item -Path $BackupDir -ItemType Directory -Force | Out-Null
}

if (Test-Path "$AppDir\app.db") {
    Copy-Item "$AppDir\app.db" "$BackupDir\app_$timestamp.db"
    Write-Success "Backup criado: $BackupDir\app_$timestamp.db"
} else {
    Write-Warning "app.db não encontrado (primeira instalação?)"
}

# Stop IIS AppPool (if using IIS) or app process
Write-Header "Parando Aplicação"

$processName = "dotnet"
$processes = Get-Process -Name $processName -ErrorAction SilentlyContinue
if ($processes) {
    $processes | Stop-Process -Force
    Start-Sleep -Seconds 2
    Write-Success "Processo parado"
} else {
    Write-Warning "Nenhum processo dotnet encontrado"
}

# Deploy
Write-Header "Deploying Aplicação"

if (-not (Test-Path ".\publish")) {
    Write-Error "Diretório 'publish' não encontrado. Execute 'dotnet publish -c Release' primeiro"
}

if (Test-Path $AppDir) {
    Remove-Item $AppDir -Recurse -Force
}
New-Item -Path $AppDir -ItemType Directory -Force | Out-Null

Copy-Item -Path ".\publish\*" -Destination $AppDir -Recurse -Force
Write-Success "Arquivos copiados para $AppDir"

# Create Windows Service (optional - using NSSM)
Write-Header "Configurando Serviço Windows"

$serviceName = "J4CKASS"
$serviceExists = Get-Service -Name $serviceName -ErrorAction SilentlyContinue

if (-not $serviceExists) {
    Write-Warning "Para registrar como serviço Windows, instale NSSM:"
    Write-Host "  https://nssm.cc/download" -ForegroundColor Yellow
    Write-Host "  Depois execute:" -ForegroundColor Yellow
    Write-Host "  nssm install $serviceName `"C:\Program Files\dotnet\dotnet.exe`" `"$AppDir\GrupoArmaReforger.dll`"" -ForegroundColor Yellow
    Write-Host "  nssm set $serviceName AppDirectory `"$AppDir`"" -ForegroundColor Yellow
} else {
    Write-Success "Serviço Windows '$serviceName' já existe"
}

# Create startup script
Write-Header "Criando Script de Inicialização"

$startupScript = @"
@echo off
title J4CKASS MILSIM
cd /d $AppDir
set ASPNETCORE_ENVIRONMENT=Production
set ADMIN_EMAIL=$env:ADMIN_EMAIL
set ADMIN_PASSWORD=$env:ADMIN_PASSWORD
dotnet GrupoArmaReforger.dll
pause
"@

$startupScript | Out-File "$AppDir\start.bat" -Encoding ASCII
Write-Success "Script de inicialização criado: $AppDir\start.bat"

# Summary
Write-Header "Deploy Completo! ✅"

Write-Host "Informações:" -ForegroundColor Cyan
Write-Host "  Aplicação: GrupoArmaReforger"
Write-Host "  Diretório: $AppDir"
Write-Host ""
Write-Host "Para iniciar a aplicação:" -ForegroundColor Cyan
Write-Host "  Clique em: $AppDir\start.bat" -ForegroundColor Yellow
Write-Host "  Ou execute: dotnet $AppDir\GrupoArmaReforger.dll" -ForegroundColor Yellow
Write-Host ""
Write-Host "Acesse:" -ForegroundColor Cyan
Write-Host "  http://localhost:5000/Admin/Login" -ForegroundColor Yellow
Write-Host "  Email: $env:ADMIN_EMAIL" -ForegroundColor Yellow
Write-Host ""
Write-Success "Bem-vindo ao J4CKASS MILSIM!"
