using GrupoArmaReforger.Core.Domain;
using BCrypt.Net;

namespace GrupoArmaReforger.Infrastructure.Data;

/// <summary>
/// Inicializador do banco de dados com dados padrão
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Popula dados iniciais no banco de dados se estiver vazio
    /// </summary>
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Aplicar migrations pendentes
        await context.Database.EnsureCreatedAsync();

        // Se já tem admins, não faz nada
        if (context.AdminUsers.Any())
            return;

        // Criar admin padrão (credenciais devem ser definidas via variáveis de ambiente)
        var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
        var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        if (string.IsNullOrWhiteSpace(adminPassword) || string.IsNullOrWhiteSpace(adminEmail))
        {
            throw new InvalidOperationException(
                "ADMIN_EMAIL e ADMIN_PASSWORD devem ser definidos via variáveis de ambiente antes do primeiro start.");
        }

        var admin = new AdminUser
        {
            Email = adminEmail,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(adminPassword, workFactor: 12),
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        context.AdminUsers.Add(admin);
        await context.SaveChangesAsync();

        // Adicionar aviso inicial
        var avisoInicial = new Aviso
        {
            Titulo = "Bem-vindo ao J4CKASS MILSIM",
            Conteudo = "Bem-vindo ao novo portal web da comunidade J4CKASS MILSIM! Este é o hub central onde você encontrará todas as informações sobre regras, avisos de operações, atualizações do projeto e informações sobre recrutamento.",
            AdminUserId = admin.Id,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        context.Avisos.Add(avisoInicial);
        await context.SaveChangesAsync();

        // Adicionar atualização inicial
        var atualizacaoInicial = new Atualizacao
        {
            Versao = "v1.0.0",
            Conteudo = "🎉 Lançamento oficial do portal web!\n\nNovidades:\n- Portal web centralizado para a comunidade\n- Página de regras com código de conduta\n- Sistema de avisos e comunicados\n- Página de recrutamento integrada\n- Informações sobre o grupo e valores\n- Design responsivo para mobile e desktop\n- Painel administrativo com autenticação\n\nObrigado por fazer parte do J4CKASS MILSIM!",
            AdminUserId = admin.Id,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        context.Atualizacoes.Add(atualizacaoInicial);
        await context.SaveChangesAsync();
    }
}
