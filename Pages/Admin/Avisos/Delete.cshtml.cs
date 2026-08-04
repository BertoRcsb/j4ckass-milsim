using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Avisos;

/// <summary>
/// PageModel para deletar aviso
/// </summary>
[Authorize]
public class AvisosDeleteModel : PageModel
{
    private readonly AvisoService _avisoService;

    [FromQuery]
    public int Id { get; set; }

    public AvisoExibicaoDTO? Aviso { get; set; }

    public AvisosDeleteModel(AvisoService avisoService)
    {
        _avisoService = avisoService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Aviso = await _avisoService.ObterPorIdAsync(id);
        if (Aviso == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var resultado = await _avisoService.RemoverAsync(id);

        if (!resultado.Sucesso)
        {
            ModelState.AddModelError("", resultado.Mensagem);
            Aviso = await _avisoService.ObterPorIdAsync(id);
            return Page();
        }

        return RedirectToPage("/Admin/Avisos/Index");
    }
}
