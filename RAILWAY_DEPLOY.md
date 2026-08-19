# Deploy rápido no Railway (5 min)

## 🚀 Passo a passo (copie exatamente)

### 1. Ir para https://railway.app
- Clique em **"Create New Project"**
- Selecione **"Deploy from GitHub"**

### 2. Conectar seu GitHub
- Clique em **"Configure GitHub App"**
- Autorize Railway no seu GitHub
- Selecione o repositório: **BertoRcsb/j4ckass-milsim**

### 3. Railway detecta automaticamente
- Railway lê `railway.json` + `Dockerfile`
- Clica em **"Deploy"**
- Aguarda ~2 min (building + starting)

### 4. Variáveis de ambiente
- No painel do Railway, vá para **"Variables"**
- Adicione:
```
ADMIN_EMAIL=Ronancostasgtr@gmail.com
ADMIN_PASSWORD=SuaSenhaForte123
```

### 5. Pronto!
- Railway gera URL automática: `seu-projeto.railway.app`
- Seu site fica **no ar em ~3 min**

---

## 📝 Após deploy

- **Home:** https://seu-projeto.railway.app
- **Admin:** https://seu-projeto.railway.app/Admin/Login
- **Email:** Ronancostasgtr@gmail.com
- **Senha:** (a que você colocou nas variables)

---

## 🛑 Se der erro

Procure no **"Logs"** do Railway por erros. Geralmente é variável de ambiente faltando.

---

**Depois que tiver o link, manda aqui que eu testo tudo!**
