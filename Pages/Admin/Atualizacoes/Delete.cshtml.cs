using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Atualizacoes;

/// <summary>
/// PageModel para deletar atualização
/// </summary>
[Authorize]
public class AtualizacoesDeleteModel : PageModel
{
    private readonly AtualizacaoService _atualizacaoService;

    [FromQuery]
    public int Id { get; set; }

    public AtualizacaoExibicaoDTO? Atualizacao { get; set; }

    public AtualizacoesDeleteModel(AtualizacaoService atualizacaoService)
    {
        _atualizacaoService = atualizacaoService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Atualizacao = await _atualizacaoService.ObterPorIdAsync(id);
        if (Atualizacao == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var resultado = await _atualizacaoService.RemoverAsync(id);

        if (!resultado.Sucesso)
        {
            ModelState.AddModelError("", resultado.Mensagem);
            Atualizacao = await _atualizacaoService.ObterPorIdAsync(id);
            return Page();
        }

        return RedirectToPage("/Admin/Atualizacoes/Index");
    }
}
