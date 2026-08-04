using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Atualizacoes;

/// <summary>
/// PageModel para listar atualizações
/// </summary>
[Authorize]
public class AtualizacoesIndexModel : PageModel
{
    private readonly AtualizacaoService _atualizacaoService;

    public IEnumerable<AtualizacaoExibicaoDTO> Atualizacoes { get; set; } = Enumerable.Empty<AtualizacaoExibicaoDTO>();

    public AtualizacoesIndexModel(AtualizacaoService atualizacaoService)
    {
        _atualizacaoService = atualizacaoService;
    }

    public async Task OnGetAsync()
    {
        Atualizacoes = await _atualizacaoService.ObterTodosAsync();
    }
}
