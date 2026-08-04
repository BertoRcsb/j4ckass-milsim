# Guia de Deploy - J4CKASS MILSIM

## Pré-requisitos

- .NET 9.0 SDK instalado
- Git instalado
- Servidor web ou plataforma de hosting (Azure, Heroku, VPS, etc)

---

## Build Local

```bash
# Restaurar dependências
dotnet restore

# Build em debug
dotnet build

# Build em release (otimizado)
dotnet publish -c Release -o ./publish
```

---

## Executar Localmente

### Desenvolvimento

```bash
# Com reload automático
dotnet watch run

# Ou simplesmente
dotnet run

# Acesse: https://localhost:5001
```

### Produção Local

```bash
cd publish
dotnet GrupoArmaReforger.dll
```

---

## Variáveis de Ambiente

Criar arquivo `.env` ou configurar no servidor:

```bash
# Admin padrão (obrigatório alterar em produção)
ADMIN_EMAIL=admin@j4ckass.local
ADMIN_PASSWORD=Admin@12345

# Banco de dados (opcional)
# Padrão: app.db (SQLite)
```

---

## Deploy em Azure App Service

```bash
# 1. Criar grupo de recursos
az group create --name j4ckass-rg --location eastus

# 2. Criar App Service Plan
az appservice plan create \
  --name j4ckass-plan \
  --resource-group j4ckass-rg \
  --sku B1

# 3. Criar Web App
az webapp create \
  --resource-group j4ckass-rg \
  --plan j4ckass-plan \
  --name j4ckass-milsim \
  --runtime "dotnet|9.0"

# 4. Configurar variáveis de ambiente
az webapp config appsettings set \
  --resource-group j4ckass-rg \
  --name j4ckass-milsim \
  --settings ADMIN_EMAIL="seu-email@j4ckass.com" \
             ADMIN_PASSWORD="SenhaForte123!"

# 5. Deploy via GitHub Actions ou manual
az webapp deployment source config-zip \
  --resource-group j4ckass-rg \
  --name j4ckass-milsim \
  --src ./publish.zip
```

---

## Deploy via GitHub Actions

Criar arquivo `.github/workflows/deploy.yml`:

```yaml
name: Deploy J4CKASS MILSIM

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0.x'
    
    - name: Build
      run: dotnet publish -c Release -o publish
    
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: j4ckass-milsim
        publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
        package: ./publish
```

---

## Deploy em VPS (Linux)

```bash
# 1. SSH no servidor
ssh usuario@seu-vps.com

# 2. Instalar .NET 9.0
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --version 9.0

# 3. Clonar repositório
git clone https://github.com/seu-usuario/J4CKASS-MILSIM.git
cd J4CKASS-MILSIM

# 4. Build
dotnet publish -c Release -o ./publish

# 5. Configurar systemd service
sudo tee /etc/systemd/system/j4ckass.service > /dev/null <<EOF
[Unit]
Description=J4CKASS MILSIM Portal
After=network.target

[Service]
User=www-data
WorkingDirectory=/var/www/j4ckass
ExecStart=/usr/bin/dotnet /var/www/j4ckass/GrupoArmaReforger.dll
Restart=always
RestartSec=10

Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ADMIN_EMAIL=admin@j4ckass.local"
Environment="ADMIN_PASSWORD=SenhaSegura123!"

[Install]
WantedBy=multi-user.target
EOF

# 6. Ativar serviço
sudo systemctl daemon-reload
sudo systemctl enable j4ckass
sudo systemctl start j4ckass

# 7. Verificar status
sudo systemctl status j4ckass

# 8. Ver logs
sudo journalctl -u j4ckass -f
```

---

## Nginx Reverse Proxy

```nginx
server {
    listen 80;
    server_name j4ckass-milsim.com www.j4ckass-milsim.com;

    # Redirecionar HTTP para HTTPS
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name j4ckass-milsim.com www.j4ckass-milsim.com;

    # Certificado SSL (Let's Encrypt)
    ssl_certificate /etc/letsencrypt/live/j4ckass-milsim.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/j4ckass-milsim.com/privkey.pem;

    # Headers de segurança
    add_header Strict-Transport-Security "max-age=31536000; includeSubDomains" always;
    add_header X-Content-Type-Options "nosniff" always;
    add_header X-Frame-Options "SAMEORIGIN" always;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
}
```

---

## Backup do Banco de Dados

```bash
# Backup manual
cp app.db app.db.backup.$(date +%Y%m%d_%H%M%S)

# Script de backup automático (cron job)
0 2 * * * cp /var/www/j4ckass/app.db /backups/app.db.$(date +\%Y\%m\%d)
```

---

## Checklist de Deploy

- [ ] Variáveis de ambiente configuradas
- [ ] Banco de dados migrado (migrations aplicadas)
- [ ] Admin padrão criado
- [ ] HTTPS certificado instalado
- [ ] HSTS headers configurados
- [ ] Backup automático do BD configurado
- [ ] Logs sendo armazenados corretamente
- [ ] Health check funcionando
- [ ] Performance otimizada (minified CSS/JS)
- [ ] Testes passando

---

## Monitoramento

### Logs

```bash
# Ver logs em produção
dotnet GrupoArmaReforger.dll 2>&1 | tee app.log

# Arquivo de log estruturado
tail -f app.log | grep "ERROR"
```

### Health Check

```bash
curl https://j4ckass-milsim.com/
# Retorna status 200 OK
```

### Performance

Ferramentas recomendadas:
- **Google PageSpeed Insights** - Análise de performance
- **New Relic** ou **Application Insights** - Monitoramento
- **Sentry** - Error tracking

---

## Troubleshooting

### Erro: "Erro ao processar operação"

Verifique:
- Banco de dados está acessível
- Variáveis de ambiente configuradas
- Pasta de escrita tem permissões

```bash
# Verificar permissões
ls -la app.db
chmod 666 app.db
```

### Erro: "Admin não encontrado"

Execute o DbInitializer:

```bash
dotnet run --seed
# Ou recrie o banco:
rm app.db
dotnet run
```

### Erro de Autenticação

1. Verifique ADMIN_EMAIL e ADMIN_PASSWORD
2. Limpe cookies do navegador
3. Verifique logs de erro

```bash
grep -i "auth" app.log
```

---

## Rollback

Se algo der errado:

```bash
# Voltar para versão anterior
git revert HEAD
dotnet publish -c Release
# Deploy novamente
```

---

## Contato e Suporte

- **GitHub:** https://github.com/seu-usuario/J4CKASS-MILSIM
- **Discord:** https://discord.gg/j4ckass
- **Email:** admin@j4ckass.local
