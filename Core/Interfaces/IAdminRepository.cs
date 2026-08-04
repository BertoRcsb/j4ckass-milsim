using GrupoArmaReforger.Core.Domain;

namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Contrato para operações de persistência de Administradores
/// </summary>
public interface IAdminRepository
{
    /// <summary>
    /// Obtém um administrador pelo email
    /// </summary>
    Task<AdminUser?> ObterPorEmailAsync(string email);

    /// <summary>
    /// Obtém um administrador pelo ID
    /// </summary>
    Task<AdminUser?> ObterPorIdAsync(int id);

    /// <summary>
    /// Verifica se um email já está cadastrado
    /// </summary>
    Task<bool> ExisteEmailAsync(string email);

    /// <summary>
    /// Adiciona um novo administrador
    /// </summary>
    Task<AdminUser> AdicionarAsync(AdminUser adminUser);

    /// <summary>
    /// Atualiza um administrador existente
    /// </summary>
    Task<AdminUser> AtualizarAsync(AdminUser adminUser);
}
