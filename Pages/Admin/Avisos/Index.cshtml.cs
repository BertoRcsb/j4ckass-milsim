using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Avisos;

/// <summary>
/// PageModel para listar avisos
/// </summary>
[Authorize]
public class AvisosIndexModel : PageModel
{
    private readonly AvisoService _avisoService;

    public IEnumerable<AvisoExibicaoDTO> Avisos { get; set; } = Enumerable.Empty<AvisoExibicaoDTO>();

    public AvisosIndexModel(AvisoService avisoService)
    {
        _avisoService = avisoService;
    }

    public async Task OnGetAsync()
    {
        Avisos = await _avisoService.ObterTodosAsync();
    }
}
