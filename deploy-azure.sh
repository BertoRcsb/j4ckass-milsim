#!/bin/bash

# 🚀 Script de Deploy Automático - Azure App Service
# J4CKASS MILSIM Portal

set -e

echo "=========================================="
echo "🚀 DEPLOY AUTOMÁTICO - AZURE APP SERVICE"
echo "=========================================="
echo ""

# Cores
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Configurações
RESOURCE_GROUP="j4ckass-rg"
APP_SERVICE_PLAN="j4ckass-plan"
APP_NAME="j4ckass-milsim"
LOCATION="brazilsouth"
ADMIN_EMAIL="admin@j4ckass.local"
ADMIN_PASSWORD="Admin@12345"

echo -e "${YELLOW}[1/7]${NC} Verificando Azure CLI..."
if ! command -v az &> /dev/null; then
    echo -e "${RED}❌ Azure CLI não está instalado${NC}"
    echo "Instale com: curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash"
    exit 1
fi
echo -e "${GREEN}✅ Azure CLI encontrado${NC}"
echo ""

echo -e "${YELLOW}[2/7]${NC} Verificando login no Azure..."
if ! az account show &> /dev/null; then
    echo -e "${YELLOW}⚠️  Você não está logado. Abrindo navegador...${NC}"
    az login --use-device-code
fi
ACCOUNT=$(az account show --query "user.name" -o tsv)
echo -e "${GREEN}✅ Logado como: $ACCOUNT${NC}"
echo ""

echo -e "${YELLOW}[3/7]${NC} Criando Grupo de Recursos: $RESOURCE_GROUP..."
az group create \
  --name $RESOURCE_GROUP \
  --location $LOCATION \
  --output none 2>/dev/null || echo "✅ Grupo já existe"
echo -e "${GREEN}✅ Grupo de recursos pronto${NC}"
echo ""

echo -e "${YELLOW}[4/7]${NC} Criando App Service Plan (FREE)..."
az appservice plan create \
  --name $APP_SERVICE_PLAN \
  --resource-group $RESOURCE_GROUP \
  --sku FREE \
  --output none 2>/dev/null || echo "✅ Plan já existe"
echo -e "${GREEN}✅ App Service Plan pronto${NC}"
echo ""

echo -e "${YELLOW}[5/7]${NC} Criando Web App: $APP_NAME..."
az webapp create \
  --resource-group $RESOURCE_GROUP \
  --plan $APP_SERVICE_PLAN \
  --name $APP_NAME \
  --runtime "DOTNET|9.0" \
  --output none 2>/dev/null || echo "✅ Web App já existe"
echo -e "${GREEN}✅ Web App criada${NC}"
echo ""

echo -e "${YELLOW}[6/7]${NC} Configurando Variáveis de Ambiente..."
az webapp config appsettings set \
  --resource-group $RESOURCE_GROUP \
  --name $APP_NAME \
  --settings \
    ADMIN_EMAIL="$ADMIN_EMAIL" \
    ADMIN_PASSWORD="$ADMIN_PASSWORD" \
    ASPNETCORE_ENVIRONMENT="Production" \
  --output none
echo -e "${GREEN}✅ Variáveis configuradas${NC}"
echo ""

echo -e "${YELLOW}[7/7]${NC} Deploy do aplicativo..."

# Verificar se deploy.tar.gz existe
if [ ! -f "deploy.tar.gz" ]; then
    echo -e "${RED}❌ deploy.tar.gz não encontrado${NC}"
    echo "Execute primeiro: dotnet publish -c Release"
    exit 1
fi

# Para usar tar.gz, precisamos converter para zip
echo "Convertendo arquivo de deploy..."
mkdir -p deploy_temp
cd deploy_temp
tar -xzf ../deploy.tar.gz
zip -r -q ../deploy.zip .
cd ..
rm -rf deploy_temp

# Deploy
az webapp deployment source config-zip \
  --resource-group $RESOURCE_GROUP \
  --name $APP_NAME \
  --src deploy.zip \
  --output none

echo -e "${GREEN}✅ Deploy concluído!${NC}"
echo ""

echo "=========================================="
echo -e "${GREEN}🎉 SUCESSO!${NC}"
echo "=========================================="
echo ""
echo "📱 Seu site está disponível em:"
echo ""
echo -e "${GREEN}https://$APP_NAME.azurewebsites.net${NC}"
echo ""
echo "🔐 Login Admin:"
echo "   Email: $ADMIN_EMAIL"
echo "   Senha: $ADMIN_PASSWORD"
echo ""
echo "⏳ Nota: Primeira requisição leva 10-20 segundos (cold start)"
echo ""
echo "📊 Ver logs:"
echo "   az webapp log tail --resource-group $RESOURCE_GROUP --name $APP_NAME"
echo ""
echo "=========================================="
