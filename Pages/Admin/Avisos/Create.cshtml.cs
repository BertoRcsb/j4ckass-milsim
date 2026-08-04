using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Avisos;

/// <summary>
/// PageModel para criar novo aviso
/// </summary>
[Authorize]
public class AvisosCreateModel : PageModel
{
    private readonly AvisoService _avisoService;

    [BindProperty]
    public AvisoDTO Aviso { get; set; } = new();

    public string SuccessMessage { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    public AvisosCreateModel(AvisoService avisoService)
    {
        _avisoService = avisoService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        if (adminId == 0)
        {
            ErrorMessage = "Erro ao obter dados do usuário autenticado";
            return Page();
        }

        var resultado = await _avisoService.CriarAsync(Aviso, adminId);

        if (!resultado.Sucesso)
        {
            ErrorMessage = resultado.Mensagem;
            return Page();
        }

        return RedirectToPage("/Admin/Avisos/Index");
    }
}
