# 🚀 Deploy Rápido no Azure (3 Passos)

## ✅ Passo 1: Criar Conta Azure Grátis

Acesse: https://azure.microsoft.com/pt-br/free/

1. Clique "Começar Gratuitamente"
2. Faça login com Microsoft ou crie uma conta
3. Valide com cartão de crédito (NÃO cobra nada!)
4. Você ganha R$ 250 de crédito + 12 meses grátis

**Tempo: 5 minutos**

---

## ✅ Passo 2: Instalar Azure CLI

Abra o terminal e execute:

```bash
curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
```

Verifique se funcionou:
```bash
az --version
```

**Tempo: 3 minutos**

---

## ✅ Passo 3: Rodar o Script de Deploy

Na pasta do projeto, execute:

```bash
cd /home/ronan/J4CKASS-MILSIM
./deploy-azure.sh
```

O script vai:
1. ✅ Verificar login no Azure (abrirá navegador)
2. ✅ Criar grupo de recursos
3. ✅ Criar App Service Plan (FREE)
4. ✅ Criar Web App
5. ✅ Configurar admin
6. ✅ Fazer upload da aplicação
7. ✅ Colocar online

**Tempo: 5-10 minutos**

---

## 🎉 Pronto!

Seu site estará disponível em:

```
https://j4ckass-milsim.azurewebsites.net
```

### Login Admin:
- **Email:** admin@j4ckass.local
- **Senha:** Admin@12345

### Ver Logs:
```bash
az webapp log tail --resource-group j4ckass-rg --name j4ckass-milsim
```

---

## 📊 Limites do Free Tier

- ⏰ 60 minutos/dia de CPU
- 💾 1 GB de RAM
- 🌐 Acesso 24/7 (mas dorme se não usar)
- 🚀 Primeira requisição = 10-20 seg (cold start)

Para **24/7 sem limite**, upgrade para ~R$ 50/mês

---

## ⚠️ Se der erro no script

### Erro: "Azure CLI não está instalado"
```bash
# Tente instalar manualmente
curl -sL https://aka.ms/InstallAzureCLIDeb | bash
```

### Erro: "Não está logado"
```bash
# Faça login
az login --use-device-code
```

### Erro: "deploy.zip não encontrado"
```bash
# Faça o publish primeiro
dotnet publish -c Release
```

---

## 🔄 Para atualizar o site depois

Sempre que fizer mudanças:

```bash
# 1. Fazer publish
dotnet publish -c Release

# 2. Rodar o deploy novamente
./deploy-azure.sh
```

---

**Está pronto? Execute o Passo 3! 🚀**
