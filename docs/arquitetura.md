# Arquitetura

## Padrao

O projeto deve seguir separacao em camadas baseada em Clean Architecture.

## Projetos sugeridos

```text
J4ckassMilsim.Domain
J4ckassMilsim.Application
J4ckassMilsim.Infrastructure
J4ckassMilsim.Web
J4ckassMilsim.UnitTests
J4ckassMilsim.IntegrationTests
```

## Domain

Responsavel por:

- entidades;
- regras de negocio;
- conceitos centrais do sistema.

Exemplos:

```text
Domain/
├── Entities/
│   ├── Notice.cs
│   ├── Update.cs
│   └── AdminUser.cs
```

## Application

Responsavel por:

- casos de uso;
- servicos de aplicacao;
- DTOs;
- interfaces de repositorios;
- validacoes de regras de aplicacao.

Exemplos:

```text
Application/
├── DTOs/
├── Interfaces/
├── Services/
└── UseCases/
```

## Infrastructure

Responsavel por:

- banco de dados;
- Entity Framework Core;
- SQLite;
- persistencia;
- repositorios concretos;
- configuracoes de logs;
- migrations.

Exemplos:

```text
Infrastructure/
├── Data/
│   ├── AppDbContext.cs
│   └── Migrations/
├── Repositories/
└── Logging/
```

## Web

Responsavel por:

- controllers;
- views;
- CSS;
- JavaScript;
- arquivos estaticos;
- autenticacao;
- filtros;
- configuracao HTTP;
- Bootstrap.

Exemplos:

```text
Web/
├── Controllers/
├── Areas/
│   └── Admin/
├── Views/
├── ViewModels/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
│       └── backgrounds/
├── appsettings.json
└── Program.cs
```

## Identidade visual

Tema:

- MILSIM moderno;
- escuro;
- militar;
- elegante;
- profissional.

Paleta:

- verde escuro;
- preto;
- cinza tatico;
- branco.

Sugestao inicial:

```css
--color-bg: #0b0f0c;
--color-surface: #151a16;
--color-primary: #3f5f3b;
--color-secondary: #8a947f;
--color-text: #f4f4f0;
--color-muted: #b7b7aa;
```

## Imagens

Diretorio:

```text
src/J4ckassMilsim.Web/wwwroot/images/backgrounds
```

Todas as paginas devem possuir imagem de fundo relacionada ao universo militar.

Regras:

- apenas operadores brasileiros;
- apenas bandeiras brasileiras;
- nenhuma bandeira americana;
- nenhum simbolo politico;
- nenhum simbolo extremista.

As imagens devem representar:

- forcas especiais;
- equipamentos modernos;
- operacoes taticas;
- camaradagem.

As imagens devem ser configuraveis posteriormente.
