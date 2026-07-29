using GrupoArmaReforger.Core.Domain;

namespace GrupoArmaReforger.Core.Interfaces;

public interface IOperadorRepository
{
    Task<Operador> AdicionarAsync(Operador operador);
    Task<IEnumerable<Operador>> ObterTodosAsync();
    Task<Operador?> ObterPorIdAsync(int id);
    Task<bool> ExisteEmailAsync(string email);
}
