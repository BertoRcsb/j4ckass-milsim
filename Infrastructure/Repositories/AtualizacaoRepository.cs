using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Infrastructure.Data;

namespace GrupoArmaReforger.Infrastructure.Repositories;

/// <summary>
/// Implementação de repositório para Atualizações
/// </summary>
public class AtualizacaoRepository : IAtualizacaoRepository
{
    private readonly AppDbContext _context;

    public AtualizacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém todas as atualizações ordenadas por data decrescente
    /// </summary>
    public async Task<IEnumerable<Atualizacao>> ObterTodosAsync()
    {
        return await _context.Atualizacoes
            .OrderByDescending(a => a.DataCriacao)
            .ToListAsync();
    }

    /// <summary>
    /// Obtém uma atualização pelo ID
    /// </summary>
    public async Task<Atualizacao?> ObterPorIdAsync(int id)
    {
        return await _context.Atualizacoes
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Adiciona uma nova atualização
    /// </summary>
    public async Task<Atualizacao> AdicionarAsync(Atualizacao atualizacao)
    {
        _context.Atualizacoes.Add(atualizacao);
        await _context.SaveChangesAsync();
        return atualizacao;
    }

    /// <summary>
    /// Atualiza uma atualização existente
    /// </summary>
    public async Task<Atualizacao> AtualizarAsync(Atualizacao atualizacao)
    {
        atualizacao.DataAtualizacao = DateTime.UtcNow;
        _context.Atualizacoes.Update(atualizacao);
        await _context.SaveChangesAsync();
        return atualizacao;
    }

    /// <summary>
    /// Remove uma atualização pelo ID
    /// </summary>
    public async Task<bool> RemoverAsync(int id)
    {
        var atualizacao = await _context.Atualizacoes.FindAsync(id);
        if (atualizacao == null)
            return false;

        _context.Atualizacoes.Remove(atualizacao);
        await _context.SaveChangesAsync();
        return true;
    }
}
