using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Infrastructure.Data;

namespace GrupoArmaReforger.Infrastructure.Repositories;

/// <summary>
/// Implementação de repositório para Administradores
/// </summary>
public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém um administrador pelo email
    /// </summary>
    public async Task<AdminUser?> ObterPorEmailAsync(string email)
    {
        return await _context.AdminUsers
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    /// <summary>
    /// Obtém um administrador pelo ID
    /// </summary>
    public async Task<AdminUser?> ObterPorIdAsync(int id)
    {
        return await _context.AdminUsers
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Verifica se um email já está cadastrado
    /// </summary>
    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.AdminUsers
            .AnyAsync(a => a.Email == email);
    }

    /// <summary>
    /// Adiciona um novo administrador
    /// </summary>
    public async Task<AdminUser> AdicionarAsync(AdminUser adminUser)
    {
        _context.AdminUsers.Add(adminUser);
        await _context.SaveChangesAsync();
        return adminUser;
    }

    /// <summary>
    /// Atualiza um administrador existente
    /// </summary>
    public async Task<AdminUser> AtualizarAsync(AdminUser adminUser)
    {
        _context.AdminUsers.Update(adminUser);
        await _context.SaveChangesAsync();
        return adminUser;
    }
}
