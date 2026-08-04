using GrupoArmaReforger.Core.Domain;

namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Contrato para operações de persistência de Atualizações
/// </summary>
public interface IAtualizacaoRepository
{
    /// <summary>
    /// Obtém todas as atualizações ordenadas por data decrescente
    /// </summary>
    Task<IEnumerable<Atualizacao>> ObterTodosAsync();

    /// <summary>
    /// Obtém uma atualização pelo ID
    /// </summary>
    Task<Atualizacao?> ObterPorIdAsync(int id);

    /// <summary>
    /// Adiciona uma nova atualização
    /// </summary>
    Task<Atualizacao> AdicionarAsync(Atualizacao atualizacao);

    /// <summary>
    /// Atualiza uma atualização existente
    /// </summary>
    Task<Atualizacao> AtualizarAsync(Atualizacao atualizacao);

    /// <summary>
    /// Remove uma atualização pelo ID
    /// </summary>
    Task<bool> RemoverAsync(int id);
}
