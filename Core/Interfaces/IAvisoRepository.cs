using GrupoArmaReforger.Core.Domain;

namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Contrato para operações de persistência de Avisos
/// </summary>
public interface IAvisoRepository
{
    /// <summary>
    /// Obtém todos os avisos ordenados por data decrescente
    /// </summary>
    Task<IEnumerable<Aviso>> ObterTodosAsync();

    /// <summary>
    /// Obtém um aviso pelo ID
    /// </summary>
    Task<Aviso?> ObterPorIdAsync(int id);

    /// <summary>
    /// Adiciona um novo aviso
    /// </summary>
    Task<Aviso> AdicionarAsync(Aviso aviso);

    /// <summary>
    /// Atualiza um aviso existente
    /// </summary>
    Task<Aviso> AtualizarAsync(Aviso aviso);

    /// <summary>
    /// Remove um aviso pelo ID
    /// </summary>
    Task<bool> RemoverAsync(int id);
}
