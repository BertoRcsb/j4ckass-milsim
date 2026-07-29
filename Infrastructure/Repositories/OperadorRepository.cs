using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Infrastructure.Data;

namespace GrupoArmaReforger.Infrastructure.Repositories;

/// <summary>
/// Repositório de Operador - Implementação do padrão Repository
/// Gerencia todas as operações de persistência de Operador no banco de dados
/// </summary>
public class OperadorRepository : IOperadorRepository
{
    private readonly AppDbContext _context;

    public OperadorRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona um novo operador ao banco de dados
    /// </summary>
    /// <param name="operador">Operador a ser persistido</param>
    /// <returns>Operador com ID preenchido após inserção</returns>
    public async Task<Operador> AdicionarAsync(Operador operador)
    {
        _context.Operadores.Add(operador);
        await _context.SaveChangesAsync();
        return operador;
    }

    /// <summary>
    /// Obtém todos os operadores ordenados por data de criação (mais recentes primeiro)
    /// </summary>
    /// <returns>Lista de todos os operadores cadastrados</returns>
    public async Task<IEnumerable<Operador>> ObterTodosAsync()
    {
        return await _context.Operadores
            .OrderByDescending(o => o.DataCriacao)
            .ToListAsync();
    }

    /// <summary>
    /// Obtém um operador específico pelo ID
    /// </summary>
    /// <param name="id">ID do operador procurado</param>
    /// <returns>Operador encontrado ou null se não existir</returns>
    public async Task<Operador?> ObterPorIdAsync(int id)
    {
        return await _context.Operadores.FirstOrDefaultAsync(o => o.Id == id);
    }

    /// <summary>
    /// Verifica se um email já está cadastrado no banco (case-insensitive)
    /// Utilizado para evitar duplicação de emails
    /// </summary>
    /// <param name="email">Email a verificar</param>
    /// <returns>True se email existe, False caso contrário</returns>
    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.Operadores
            .AnyAsync(o => o.Email.ToLower() == email.ToLower());
    }
}
