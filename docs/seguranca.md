# Seguranca

Seguranca deve ser implementada desde a primeira versao.

## Requisitos obrigatorios

- HTTPS habilitado
- Validacao de entrada em todos os formularios
- Anti-CSRF
- Headers de seguranca
- Rate Limiting
- Sanitizacao de dados
- Protecao contra XSS
- Protecao contra SQL Injection utilizando Entity Framework Core
- Secrets fora do codigo
- Sem senhas armazenadas em texto puro
- Sem exposicao de informacoes internas em erros
- Logs estruturados
- Principio do menor privilegio

## Privacidade

Nenhuma informacao pessoal deve ficar publica.

Nao publicar:

- dados de membros;
- senhas;
- tokens;
- cookies;
- hashes;
- URLs privadas, se forem sensiveis;
- arquivos de banco local;
- logs.

## Administrador

O usuario administrador inicial deve ser criado usando:

- User Secrets em desenvolvimento; ou
- variaveis de ambiente em producao.

As credenciais nunca devem ser hardcoded.

## Senhas

Senhas devem ser armazenadas apenas como hash seguro.

Nunca registrar em log:

- senha;
- hash de senha;
- token;
- cookie;
- cabecalhos sensiveis.

## Logs

Registrar:

- login administrativo;
- falhas de login;
- inclusao de avisos;
- exclusao de avisos;
- erros da aplicacao.

Nunca registrar senhas.

## Erros

Em producao, o usuario deve ver apenas mensagens genericas.

Detalhes tecnicos devem ir apenas para logs internos.

## Git

Arquivos sensiveis devem ficar fora do repositorio.

Exemplos:

```text
appsettings.Development.json
.env
*.db
logs/
```
