using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Infrastructure.Data;

namespace GrupoArmaReforger.Infrastructure.Repositories;

public class OperadorRepository : IOperadorRepository
{
    private readonly AppDbContext _context;

    public OperadorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Operador> AdicionarAsync(Operador operador)
    {
        _context.Operadores.Add(operador);
        await _context.SaveChangesAsync();
        return operador;
    }

    public async Task<IEnumerable<Operador>> ObterTodosAsync()
    {
        return await _context.Operadores
            .OrderByDescending(o => o.DataCriacao)
            .ToListAsync();
    }

    public async Task<Operador?> ObterPorIdAsync(int id)
    {
        return await _context.Operadores.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.Operadores
            .AnyAsync(o => o.Email.ToLower() == email.ToLower());
    }
}
