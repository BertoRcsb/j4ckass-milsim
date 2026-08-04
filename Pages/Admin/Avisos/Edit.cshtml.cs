using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Avisos;

/// <summary>
/// PageModel para editar aviso existente
/// </summary>
[Authorize]
public class AvisosEditModel : PageModel
{
    private readonly AvisoService _avisoService;

    [FromQuery]
    public int Id { get; set; }

    public int AvisoId { get; set; }

    [BindProperty]
    public AvisoDTO Aviso { get; set; } = new();

    public string ErrorMessage { get; set; } = string.Empty;

    public AvisosEditModel(AvisoService avisoService)
    {
        _avisoService = avisoService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var aviso = await _avisoService.ObterPorIdAsync(id);
        if (aviso == null)
            return NotFound();

        AvisoId = id;
        Aviso = new AvisoDTO
        {
            Id = aviso.Id,
            Titulo = aviso.Titulo,
            Conteudo = aviso.Conteudo
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            AvisoId = id;
            return Page();
        }

        var resultado = await _avisoService.AtualizarAsync(id, Aviso);

        if (!resultado.Sucesso)
        {
            ErrorMessage = resultado.Mensagem;
            AvisoId = id;
            return Page();
        }

        return RedirectToPage("/Admin/Avisos/Index");
    }
}
