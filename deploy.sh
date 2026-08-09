#!/bin/bash

#  ╔═══════════════════════════════════════════════════════════════╗
#  ║  J4CKASS MILSIM - Deploy Script v1.0                          ║
#  ║  Deployment automático para Linux/Unix                        ║
#  ╚═══════════════════════════════════════════════════════════════╝

set -e

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

# Config
PROJECT_NAME="GrupoArmaReforger"
APP_DIR="/var/lib/j4ckass"
SERVICE_NAME="j4ckass-app"
BACKUP_DIR="/var/backups/j4ckass"
TIMESTAMP=$(date +%Y%m%d_%H%M%S)

# Print with color
print_header() {
    echo -e "\n${BLUE}═══════════════════════════════════════════════════════════════${NC}"
    echo -e "${BLUE}$1${NC}"
    echo -e "${BLUE}═══════════════════════════════════════════════════════════════${NC}\n"
}

print_success() {
    echo -e "${GREEN}✓ $1${NC}"
}

print_error() {
    echo -e "${RED}✗ $1${NC}"
    exit 1
}

print_warning() {
    echo -e "${YELLOW}⚠ $1${NC}"
}

# Check root
if [ "$EUID" -ne 0 ]; then
    print_error "Este script deve ser executado como root (sudo)"
fi

# Check .NET
print_header "Verificando Dependências"

if ! command -v dotnet &> /dev/null; then
    print_error ".NET Runtime não encontrado. Instale .NET 9.0 Runtime."
fi

DOTNET_VERSION=$(dotnet --version)
print_success ".NET $DOTNET_VERSION instalado"

# Check environment variables
if [ -z "$ADMIN_EMAIL" ] || [ -z "$ADMIN_PASSWORD" ]; then
    print_warning "Variáveis de ambiente não definidas:"
    echo "  ADMIN_EMAIL=$ADMIN_EMAIL"
    echo "  ADMIN_PASSWORD=$ADMIN_PASSWORD"
    echo ""
    echo "Execute antes:"
    echo "  export ADMIN_EMAIL=seu-email@dominio.com"
    echo "  export ADMIN_PASSWORD=SenhaForte!123"
    echo ""
    read -p "Continuar mesmo assim? (s/n) " -n 1 -r
    echo
    [[ ! $REPLY =~ ^[Ss]$ ]] && exit 1
fi

# Backup
print_header "Backup do Banco de Dados"

mkdir -p "$BACKUP_DIR"

if [ -f "$APP_DIR/app.db" ]; then
    cp "$APP_DIR/app.db" "$BACKUP_DIR/app_${TIMESTAMP}.db"
    print_success "Backup criado: $BACKUP_DIR/app_${TIMESTAMP}.db"
else
    print_warning "app.db não encontrado (primeira instalação?)"
fi

# Stop service
print_header "Parando Serviço"

if systemctl is-active --quiet $SERVICE_NAME; then
    systemctl stop $SERVICE_NAME
    sleep 2
    print_success "Serviço parado"
else
    print_warning "Serviço não está rodando"
fi

# Build
print_header "Compilando Projeto"

if [ ! -d "publish" ]; then
    print_error "Diretório 'publish' não encontrado. Execute 'dotnet publish -c Release' primeiro"
fi

print_success "Build já existe em ./publish"

# Deploy
print_header "Deploying Aplicação"

# Remove old files
rm -rf "$APP_DIR"/*
mkdir -p "$APP_DIR"

# Copy new files
cp -r publish/* "$APP_DIR/"
print_success "Arquivos copiados para $APP_DIR"

# Set permissions
chown -R app:app "$APP_DIR" 2>/dev/null || true
chmod 755 "$APP_DIR"
print_success "Permissões configuradas"

# Create systemd service if not exists
if [ ! -f "/etc/systemd/system/$SERVICE_NAME.service" ]; then
    print_header "Criando Systemd Service"

    cat > "/etc/systemd/system/$SERVICE_NAME.service" << EOF
[Unit]
Description=$PROJECT_NAME - J4CKASS MILSIM Community Portal
After=network.target

[Service]
Type=notify
User=app
WorkingDirectory=$APP_DIR
ExecStart=/usr/bin/dotnet $APP_DIR/$PROJECT_NAME.dll
Restart=always
RestartSec=10
SyslogIdentifier=$SERVICE_NAME
Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ADMIN_EMAIL=$ADMIN_EMAIL"
Environment="ADMIN_PASSWORD=$ADMIN_PASSWORD"

[Install]
WantedBy=multi-user.target
EOF

    systemctl daemon-reload
    print_success "Systemd service criado"
else
    print_warning "Systemd service já existe. Atualizando variáveis..."
    systemctl daemon-reload
fi

# Start service
print_header "Iniciando Serviço"

systemctl start $SERVICE_NAME
sleep 3

if systemctl is-active --quiet $SERVICE_NAME; then
    print_success "Serviço iniciado com sucesso"
else
    print_error "Falha ao iniciar serviço. Verifique os logs: journalctl -xe"
fi

# Health check
print_header "Health Check"

sleep 3

if curl -f http://localhost:80/ &> /dev/null; then
    print_success "HTTP endpoint respondendo"
else
    print_warning "HTTP endpoint não respondendo. Verifique os logs."
fi

# Summary
print_header "Deploy Completo! ✅"

echo "Informações:"
echo "  Aplicação: $PROJECT_NAME"
echo "  Diretório: $APP_DIR"
echo "  Serviço: $SERVICE_NAME"
echo "  Status: $(systemctl is-active $SERVICE_NAME)"
echo ""
echo "Comandos úteis:"
echo "  Ver status:  systemctl status $SERVICE_NAME"
echo "  Ver logs:    journalctl -fu $SERVICE_NAME"
echo "  Restart:     systemctl restart $SERVICE_NAME"
echo "  Stop:        systemctl stop $SERVICE_NAME"
echo ""
echo "Acesse:"
echo "  http://localhost:80/Admin/Login"
echo "  Email: $ADMIN_EMAIL"
echo ""
print_success "Bem-vindo ao J4CKASS MILSIM!"
