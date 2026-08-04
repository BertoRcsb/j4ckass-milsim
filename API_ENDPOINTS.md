# J4CKASS MILSIM - Endpoints e Navegação

## Públicos (Sem Autenticação)

### Página Principal
- **GET** `/` → Home (hero section)
- **GET** `/Index` → Home (alias)

### Comunidade
- **GET** `/Regras` → 8 regras da comunidade
- **GET** `/Avisos` → Lista dinâmica de avisos (do banco)
- **GET** `/Atualizacoes` → Lista dinâmica de atualizações (do banco)
- **GET** `/Sobre` → Informações sobre o grupo
- **GET** `/Privacy` → Política de privacidade

### Recrutamento
- **GET** `/Recrutamento` → Formulário de recrutamento
- **POST** `/Recrutamento` → Submeter candidatura (persiste em SQLite)

---

## Admin (Requer Autenticação)

### Autenticação
- **GET** `/Admin/Login` → Página de login
- **POST** `/Admin/Login` → Autenticar com email/senha
- **GET** `/Admin/Logout` → Logout (limpa cookie)

### Dashboard
- **GET** `/Admin/Dashboard` → Painel de controle (mostra estatísticas)

### Avisos (CRUD)
- **GET** `/Admin/Avisos/Index` → Listar todos os avisos
- **GET** `/Admin/Avisos/Create` → Formulário para criar aviso
- **POST** `/Admin/Avisos/Create` → Salvar novo aviso
- **GET** `/Admin/Avisos/Edit?id={id}` → Formulário para editar
- **POST** `/Admin/Avisos/Edit` → Atualizar aviso
- **GET** `/Admin/Avisos/Delete?id={id}` → Confirmação de deleção
- **POST** `/Admin/Avisos/Delete` → Deletar aviso

### Atualizações (CRUD)
- **GET** `/Admin/Atualizacoes/Index` → Listar todas as atualizações
- **GET** `/Admin/Atualizacoes/Create` → Formulário para criar atualização
- **POST** `/Admin/Atualizacoes/Create` → Salvar nova atualização
- **GET** `/Admin/Atualizacoes/Edit?id={id}` → Formulário para editar
- **POST** `/Admin/Atualizacoes/Edit` → Atualizar atualização
- **GET** `/Admin/Atualizacoes/Delete?id={id}` → Confirmação de deleção
- **POST** `/Admin/Atualizacoes/Delete` → Deletar atualização

---

## Credenciais Padrão

**Email:** `admin@j4ckass.local`  
**Senha:** `Admin@12345`

⚠️ **Importante:** Altere as credenciais padrão em produção usando variáveis de ambiente:
```bash
export ADMIN_EMAIL="seu-email@dominio.com"
export ADMIN_PASSWORD="sua-senha-forte"
```

---

## Arquitetura de Dados

### Entidades

**AdminUser**
- id (PK)
- email (UNIQUE)
- senhaHash (BCrypt)
- ativo (boolean)
- dataCriacao
- dataUltimoLogin

**Aviso**
- id (PK)
- titulo (MAX 200 chars)
- conteudo (MAX 5000 chars)
- dataCriacao
- dataAtualizacao
- adminUserId (FK → AdminUser)

**Atualizacao**
- id (PK)
- versao (MAX 50 chars)
- conteudo (MAX 5000 chars)
- dataCriacao
- dataAtualizacao
- adminUserId (FK → AdminUser)

**Operador** (Recrutamento)
- id (PK)
- nome (MAX 100 chars)
- email (MAX 100 chars, UNIQUE)
- steamId (optional, MAX 50 chars)
- psn (optional, MAX 50 chars)
- dataCriacao

---

## Fluxo de Autenticação

1. User acessa `/Admin/Login`
2. Submete email + senha via POST
3. AdminService valida credenciais com BCrypt
4. Se válido: 
   - Cria Claims (NameIdentifier, Email, Role)
   - Gera cookie de autenticação (24 horas, sliding expiration)
   - Redireciona para `/Admin/Dashboard`
5. Se inválido: mostra erro na página

Cookie é automaticamente enviado em requisições subsequentes.  
Páginas protegidas verificam `[Authorize]` antes de executar.

---

## Stack Tecnológico

- **Framework:** ASP.NET Core 9.0 (Razor Pages)
- **Banco:** SQLite + Entity Framework Core
- **Autenticação:** Cookie-based (ASP.NET Core Authentication)
- **Hashing:** BCrypt.Net-Core v1.6.0
- **ORM:** Entity Framework Core 9.0
- **Frontend:** Bootstrap 5 + CSS3
- **Fontes:** Audiowide, Orbitron, Share Tech Mono (Google Fonts)

---

## Variáveis de Ambiente

```bash
# Email do admin padrão (default: admin@j4ckass.local)
ADMIN_EMAIL=seu-email@dominio.com

# Senha do admin padrão (default: Admin@12345)
ADMIN_PASSWORD=sua-senha-forte

# Banco de dados (default: app.db)
# Data Source=app.db
```

---

## Testes

Testes unitários para serviços críticos usando **Moq**:

```bash
# Executar testes
dotnet test

# Com coverage
dotnet test /p:CollectCoverage=true
```

Testes incluem:
- ✅ AdminService (autenticação, criação de admin)
- ✅ AvisoService (CRUD, validações)
- ✅ Validações de domínio (Entidades)

---

## Deploy em Produção

```bash
# Build em release
dotnet publish -c Release -o ./publish

# Requisitos:
# - HTTPS ativo
# - SQLite com backup automático
# - Variáveis de ambiente configuradas
# - app.db em pasta com permissões de escrita
```

---

## Segurança Implementada

✅ HTTPS redirect  
✅ HSTS headers  
✅ Entity validation  
✅ SQL parameter binding (EF Core)  
✅ Senhas com hash BCrypt (cost factor 12)  
✅ Autenticação por cookies com sliding expiration  
✅ CSRF protection (Razor Pages)  
✅ Sem secrets no repositório  
✅ Logs estruturados (sem dados sensíveis)  

---

## Próximos Passos

- [ ] Integração com Discord (webhooks)
- [ ] Dark mode theme
- [ ] Galeria de operações
- [ ] Sistema de AAR (After Action Reports)
- [ ] Email notifications
- [ ] Two-factor authentication (2FA)
- [ ] Auditoria de ações admin
