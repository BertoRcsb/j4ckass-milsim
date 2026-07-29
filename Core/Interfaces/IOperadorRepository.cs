using GrupoArmaReforger.Core.Domain;

namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Define o contrato para acesso a dados de Operador
/// </summary>
public interface IOperadorRepository
{
    /// <summary>
    /// Adiciona um novo operador ao banco de dados
    /// </summary>
    /// <param name="operador">Operador a ser adicionado</param>
    /// <returns>Operador adicionado com ID preenchido</returns>
    Task<Operador> AdicionarAsync(Operador operador);

    /// <summary>
    /// Obtém todos os operadores cadastrados
    /// </summary>
    /// <returns>Coleção de todos os operadores</returns>
    Task<IEnumerable<Operador>> ObterTodosAsync();

    /// <summary>
    /// Obtém um operador específico pelo ID
    /// </summary>
    /// <param name="id">ID do operador</param>
    /// <returns>Operador encontrado ou null</returns>
    Task<Operador?> ObterPorIdAsync(int id);

    /// <summary>
    /// Verifica se um email já está cadastrado (case-insensitive)
    /// </summary>
    /// <param name="email">Email a verificar</param>
    /// <returns>True se email existe, False caso contrário</returns>
    Task<bool> ExisteEmailAsync(string email);
}
