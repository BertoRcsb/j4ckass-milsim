# J4CKASS MILSIM

Portal oficial da comunidade J4CKASS MILSIM.

Slogan:

> MUITA PROSA, POUCA MIRA

Este projeto sera uma aplicacao web leve, gratuita para execucao local ou hospedagem de baixo custo, feita em ASP.NET Core MVC com .NET 8.

## Objetivo

Criar uma base de aprendizado e evolucao para:

- C#
- ASP.NET Core
- Banco de dados
- Seguranca
- Arquitetura Limpa
- DevOps
- Docker
- CI/CD

## Estrutura inicial

```text
J4CKASS-MILSIM/
├── docs/
├── src/
├── tests/
├── README.md
└── .gitignore
```

## Documentacao

- [Visao geral](docs/visao-geral.md)
- [Requisitos](docs/requisitos.md)
- [Arquitetura](docs/arquitetura.md)
- [Seguranca](docs/seguranca.md)
- [Backlog](docs/backlog.md)

## Como executar futuramente

Quando o projeto ASP.NET Core for criado:

```bash
dotnet restore
dotnet run --project src/J4ckassMilsim.Web
```

## Configuracao do administrador

As credenciais administrativas nao devem ficar no codigo.

Em desenvolvimento, usar User Secrets.

Em producao, usar variaveis de ambiente.

Exemplo:

```text
ADMIN_USERNAME
ADMIN_PASSWORD
```

## Publicacao

O projeto deve ser publicado inicialmente de forma simples e barata, mantendo:

- HTTPS ativo
- banco SQLite com backup
- logs sem dados sensiveis
- secrets fora do repositorio

## Proximos passos

1. Criar a solution .NET.
2. Criar os projetos Domain, Application, Infrastructure e Web.
3. Configurar SQLite, Entity Framework Core e Serilog.
4. Criar as paginas publicas.
5. Criar o painel administrativo protegido.
