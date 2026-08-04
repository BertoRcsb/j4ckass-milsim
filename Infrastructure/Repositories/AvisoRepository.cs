using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Infrastructure.Data;

namespace GrupoArmaReforger.Infrastructure.Repositories;

/// <summary>
/// Implementação de repositório para Avisos
/// </summary>
public class AvisoRepository : IAvisoRepository
{
    private readonly AppDbContext _context;

    public AvisoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém todos os avisos ordenados por data decrescente
    /// </summary>
    public async Task<IEnumerable<Aviso>> ObterTodosAsync()
    {
        return await _context.Avisos
            .OrderByDescending(a => a.DataCriacao)
            .ToListAsync();
    }

    /// <summary>
    /// Obtém um aviso pelo ID
    /// </summary>
    public async Task<Aviso?> ObterPorIdAsync(int id)
    {
        return await _context.Avisos
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Adiciona um novo aviso
    /// </summary>
    public async Task<Aviso> AdicionarAsync(Aviso aviso)
    {
        _context.Avisos.Add(aviso);
        await _context.SaveChangesAsync();
        return aviso;
    }

    /// <summary>
    /// Atualiza um aviso existente
    /// </summary>
    public async Task<Aviso> AtualizarAsync(Aviso aviso)
    {
        aviso.DataAtualizacao = DateTime.UtcNow;
        _context.Avisos.Update(aviso);
        await _context.SaveChangesAsync();
        return aviso;
    }

    /// <summary>
    /// Remove um aviso pelo ID
    /// </summary>
    public async Task<bool> RemoverAsync(int id)
    {
        var aviso = await _context.Avisos.FindAsync(id);
        if (aviso == null)
            return false;

        _context.Avisos.Remove(aviso);
        await _context.SaveChangesAsync();
        return true;
    }
}
