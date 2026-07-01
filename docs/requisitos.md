# Requisitos

## Tecnologias

### Backend

- ASP.NET Core MVC (.NET 8)

### Banco

- SQLite inicialmente

Motivos:

- gratuito;
- simples;
- leve;
- facil backup.

### ORM

- Entity Framework Core

### Frontend

- Razor Views
- Bootstrap 5

### Logs

- Serilog

## Estrutura do projeto

```text
/src
  Application
  Domain
  Infrastructure
  Web

/tests

README.md
```

## Menu principal

- Home
- Regras
- Avisos
- Atualizacoes
- Sobre
- Admin

## Pagina Home

Exibir:

- Logo J4CKASS MILSIM
- Titulo: `J4CKASS MILSIM`
- Subtitulo: `"MUITA PROSA, POUCA MIRA"`
- Descricao: `Comunidade brasileira focada em amizade, cooperacao, organizacao e operacoes taticas no Arma Reforger.`
- Botao: `Entrar no Discord`

A URL do Discord deve ser configuravel em arquivo de configuracao.

## Pagina Regras

Exibir regras da comunidade.

O conteudo sera administravel futuramente.

Inicialmente, usar arquivo estatico.

## Pagina Avisos

Exibir comunicados ordenados por data.

Exemplos:

- Operacao Domingo
- Treinamento
- Atualizacao do Servidor

## Pagina Atualizacoes

Exibir historico de mudancas.

Exemplo:

```text
Versao 1.0
- Criacao do portal

Versao 1.1
- Sistema de avisos
```

## Pagina Sobre

Explicar:

- quem somos;
- objetivos;
- historia do grupo;
- missao da comunidade.

## Painel administrativo

Acesso protegido.

Criar usuario administrador inicial.

Credenciais nao devem estar hardcoded.

Utilizar:

- User Secrets; ou
- Variaveis de Ambiente.

Funcoes:

- gerenciar avisos;
- gerenciar atualizacoes;
- gerenciar conteudo das regras.

## Banco de dados

### Tabela Notices

- Id
- Title
- Content
- CreatedAt

### Tabela Updates

- Id
- Version
- Description
- CreatedAt

### Tabela AdminUsers

- Id
- Username
- PasswordHash
- CreatedAt
