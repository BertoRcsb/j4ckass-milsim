using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin;

/// <summary>
/// PageModel para painel de controle administrativo
/// </summary>
[Authorize]
public class DashboardModel : PageModel
{
    private readonly AvisoService _avisoService;
    private readonly AtualizacaoService _atualizacaoService;

    public int TotalAvisos { get; set; }
    public int TotalAtualizacoes { get; set; }

    public DashboardModel(AvisoService avisoService, AtualizacaoService atualizacaoService)
    {
        _avisoService = avisoService;
        _atualizacaoService = atualizacaoService;
    }

    public async Task OnGetAsync()
    {
        var avisos = await _avisoService.ObterTodosAsync();
        var atualizacoes = await _atualizacaoService.ObterTodosAsync();

        TotalAvisos = avisos.Count();
        TotalAtualizacoes = atualizacoes.Count();
    }
}
