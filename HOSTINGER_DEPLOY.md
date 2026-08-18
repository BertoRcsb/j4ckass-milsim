# Deploy do J4CKASS MILSIM na Hostinger VPS (Docker + HTTPS)

1. **Contratar VPS**: painel Hostinger → VPS → plano mais barato → SO **Ubuntu 24.04** → pagar via **Pix**
   (e-mail pessoal Ronancostasgtr@gmail.com). Anotar o IP e a senha root.
2. **Domínio para HTTPS** (recomendado): criar subdomínio grátis no **DuckDNS** (duckdns.org, login Google)
   apontando para o IP do VPS. Ex.: j4ckass.duckdns.org. (Ou domínio próprio com registro A para o IP.)
3. **Acessar**: `ssh root@SEU_IP`.
4. **Instalar Docker**: `curl -fsSL https://get.docker.com | sh` (Compose v2 já vem como `docker compose`).
5. **Clonar**: `git clone https://github.com/BertoRcsb/j4ckass-milsim.git && cd j4ckass-milsim`.
6. **Segredos**: `cp .env.example .env` e `nano .env` (definir ADMIN_PASSWORD forte e SITE_DOMAIN).
7. **Subir**: `docker compose up -d --build` (Caddy emite o certificado HTTPS em ~1 min).
8. **Testar**: abrir `https://SEU_DOMINIO` (Home, Regras, Avisos, Atualizações, Recrutamento, Sobre);
   login em `https://SEU_DOMINIO/Admin/Login`; criar/editar/excluir um aviso.
9. **Atualizar depois**: `git pull && docker compose up -d --build`.
10. **Backup do banco**: copiar `./data/app.db`.
11. **Firewall**: garantir portas 80 e 443 abertas no painel Hostinger.
