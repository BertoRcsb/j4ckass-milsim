# J4CKASS MILSIM - Portal da Comunidade

Portal web para a comunidade **J4CKASS MILSIM**, um grupo brasileiro de [Arma Reforger](https://www.armaresourcelocator.com/reforger/). O site centraliza regras, avisos, atualizações e recrutamento.

## 🎯 Slogan

> **"MUITA PROSA, POUCA MIRA"**

Operações organizadas, amizade no rádio e presença tática no Arma Reforger.

## 🚀 Stack Tecnológico

- **Framework**: ASP.NET Core 9.0 (Razor Pages)
- **Banco de Dados**: SQLite + Entity Framework Core
- **Frontend**: Bootstrap 5 + CSS3
- **Padrão Arquitetural**: Clean Architecture
- **Linguagem**: C# 12

## 📁 Estrutura do Projeto

```
GrupoArmaReforger/
├── Core/                          # Domain Layer
│   ├── Domain/
│   │   └── Operador.cs           # Entidade
│   └── Interfaces/
│       ├── IOperadorRepository.cs
│       ├── IRecrutamentoService.cs
│       └── IAssetService.cs
│
├── Application/                   # Application Layer
│   ├── DTOs/
│   │   └── RecrutamentoDTO.cs
│   └── Services/
│       ├── RecrutamentoService.cs
│       └── AssetService.cs
│
├── Infrastructure/                # Infrastructure Layer
│   ├── Data/
│   │   └── AppDbContext.cs
│   └── Repositories/
│       └── OperadorRepository.cs
│
├── Pages/                         # Presentation Layer
│   ├── Index.cshtml              # Home
│   ├── Regras.cshtml             # 8 regras
│   ├── Avisos.cshtml             # Comunicados
│   ├── Atualizacoes.cshtml       # Roadmap
│   ├── Recrutamento.cshtml       # Formulário
│   └── Shared/
│       └── _Layout.cshtml
│
├── wwwroot/assets/
│   ├── logos/                    # j4novo.png, jckss.png, logo-j4.png
│   └── wallpapers/               # operations-hero.jpg, tactical-hero.jpg
│
├── Program.cs                    # Configuração DI & Startup
├── ARCHITECTURE.md               # Clean Architecture
├── CLEAN_CODE_PRACTICES.md       # Boas práticas
├── ASSETS_GUIDE.md               # Guia de imagens
└── README.md                     # Este arquivo
```

## 🏗️ Arquitetura Clean

Implementação de **Clean Architecture** com 4 camadas:

### Camadas

| Camada | Responsabilidade | Exemplo |
|--------|-----------------|---------|
| **Core** | Domínio e interfaces | `Operador`, `IRecrutamentoService` |
| **Application** | Serviços e orquestração | `RecrutamentoService`, DTOs |
| **Infrastructure** | Persistência e recursos | `OperadorRepository`, `AppDbContext` |
| **Presentation** | Interface web | `RecrutamentoModel`, Views |

### Benefícios

✅ Código testável e desacoplado
✅ Fácil manutenção e escalabilidade
✅ Reutilização de serviços
✅ Independência de frameworks

## 🔧 Execução

### Pré-requisitos

- .NET 9.0 SDK
- Visual Studio 2022 / VS Code

### Instalação & Execução

```bash
# Clone
git clone https://github.com/seu-usuario/J4CKASS-MILSIM.git
cd J4CKASS-MILSIM

# Build
dotnet restore
dotnet build

# Executar
dotnet run

# Acesse: https://localhost:5144
```

## 📋 Páginas Implementadas

| Página | Descrição |
|--------|-----------|
| **Home** | Hero section com chamada para ação |
| **Regras** | 8 regras de conduta da comunidade |
| **Avisos** | Comunicados e eventos importantes |
| **Atualizações** | Roadmap com versões planejadas |
| **Recrutamento** | Formulário com validação |
| **Sobre** | Informações do grupo |

## ✨ Funcionalidades

### ✅ Pronto

- Páginas estáticas com design responsivo
- Formulário de recrutamento com validação
- Persistência em SQLite
- Injeção de dependência
- Clean Architecture implementada
- Clean Code principles aplicados
- Sistema de assets/imagens

### 📌 Planejado

- [ ] Painel administrativo (CMS)
- [ ] Autenticação e autorização
- [ ] CRUD dinâmico de avisos
- [ ] Integração com Discord
- [ ] Dark mode
- [ ] Galeria de operações
- [ ] Sistema de AAR (After Action Reports)

## 📚 Documentação

### Arquivos Principais

- **[ARCHITECTURE.md](./ARCHITECTURE.md)** - Clean Architecture explicada
- **[CLEAN_CODE_PRACTICES.md](./CLEAN_CODE_PRACTICES.md)** - Práticas de código com exemplos
- **[ASSETS_GUIDE.md](./ASSETS_GUIDE.md)** - Organização de imagens

## 🎨 Design

- **Framework CSS**: Bootstrap 5
- **Responsivo**: Mobile-first
- **Accessibility**: Alt text, semantic HTML
- **Performance**: Lazy loading de imagens

## 🧪 Testes (Preparado para)

Estrutura pronta para testes unitários com Moq:

```csharp
[TestClass]
public class RecrutamentoServiceTests
{
    [TestMethod]
    public async Task CadastrarRecrutaAsync_ComDadosValidos_RetornaSucesso()
    {
        var mockRepo = new Mock<IOperadorRepository>();
        var service = new RecrutamentoService(mockRepo.Object, null);
        var resultado = await service.CadastrarRecrutaAsync(dto);
        Assert.IsTrue(resultado.Sucesso);
    }
}
```

## 🔐 Segurança

### Implementado

✅ HTTPS redirect
✅ HSTS headers
✅ Entity validation
✅ SQL parameter binding (EF Core)
✅ Sem secrets no repositório

### Planejado

- [ ] Authentication & Authorization
- [ ] CSRF protection
- [ ] Rate limiting
- [ ] Input sanitization

## 🚀 Deploy

### Publicar Localmente

```bash
dotnet publish -c Release -o ./publish
```

### Deploy em Produção (Hostinger VPS + Docker)

Veja [HOSTINGER_DEPLOY.md](./HOSTINGER_DEPLOY.md) para instruções completas:

- VPS Ubuntu 24.04 (Hostinger, pago via Pix)
- Docker + Docker Compose
- Caddy para HTTPS automático (Let's Encrypt)
- Subdomínio grátis via DuckDNS

**Resumo rápido:**
1. Contratar VPS Ubuntu 24.04
2. SSH para o servidor e instalar Docker
3. Clonar repositório
4. Copiar `.env.example` para `.env` e preencher credenciais
5. Rodar `docker compose up -d --build`

### Requisitos em Produção

- HTTPS ativo (Caddy gerencia certificados Let's Encrypt)
- SQLite com backup (banco em volume persistente `./data`)
- Logs estruturados, sem dados sensíveis
- Secrets via variáveis de ambiente (ADMIN_EMAIL, ADMIN_PASSWORD, SITE_DOMAIN)

## 📖 Clean Code Aplicado

### Nomes Significativos
```csharp
CadastrarRecrutaAsync()  // ✅ Claro
ExisteEmailAsync()       // ✅ Expressivo
RecrutamentoService      // ✅ Específico
```

### Funções Pequenas e Focadas
- PageModels delegam para serviços
- Serviços delegam para repositórios
- Cada classe tem uma responsabilidade

### Tratamento de Erros
```csharp
operador.Validar();  // Lança exceção se inválido
// Serviço captura e converte em DTO com mensagem
```

### DRY (Don't Repeat Yourself)
- Serviço `IAssetService` centraliza caminhos de imagens
- Evita duplicação em views

## 👥 Contribuindo

### Branches

```
main (produção)
└── develop (desenvolvimento)
    ├── feature/novo-recurso
    └── bugfix/correcao
```

### Commits

```bash
git commit -m "feat: adicionar validação de email"
git commit -m "fix: corrigir erro no PageModel"
git commit -m "refactor: simplificar RecrutamentoService"
```

## 📞 Suporte

- **Arquitetura**: [ARCHITECTURE.md](./ARCHITECTURE.md)
- **Clean Code**: [CLEAN_CODE_PRACTICES.md](./CLEAN_CODE_PRACTICES.md)
- **Assets**: [ASSETS_GUIDE.md](./ASSETS_GUIDE.md)

---

**Status**: 🟢 ✅ PRONTO PARA PRODUÇÃO
**Última atualização**: 09 Ago 2026
**Versão**: 1.0.0 Release
