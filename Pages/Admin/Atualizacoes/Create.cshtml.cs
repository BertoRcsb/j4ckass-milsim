using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Atualizacoes;

/// <summary>
/// PageModel para criar nova atualização
/// </summary>
[Authorize]
public class AtualizacoesCreateModel : PageModel
{
    private readonly AtualizacaoService _atualizacaoService;

    [BindProperty]
    public AtualizacaoDTO Atualizacao { get; set; } = new();

    public string SuccessMessage { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    public AtualizacoesCreateModel(AtualizacaoService atualizacaoService)
    {
        _atualizacaoService = atualizacaoService;
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

        var resultado = await _atualizacaoService.CriarAsync(Atualizacao, adminId);

        if (!resultado.Sucesso)
        {
            ErrorMessage = resultado.Mensagem;
            return Page();
        }

        return RedirectToPage("/Admin/Atualizacoes/Index");
    }
}
