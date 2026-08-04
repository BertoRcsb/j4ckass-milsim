using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Application.Services;

namespace GrupoArmaReforger.Pages.Admin.Atualizacoes;

/// <summary>
/// PageModel para editar atualização existente
/// </summary>
[Authorize]
public class AtualizacoesEditModel : PageModel
{
    private readonly AtualizacaoService _atualizacaoService;

    [FromQuery]
    public int Id { get; set; }

    public int AtualizacaoId { get; set; }

    [BindProperty]
    public AtualizacaoDTO Atualizacao { get; set; } = new();

    public string ErrorMessage { get; set; } = string.Empty;

    public AtualizacoesEditModel(AtualizacaoService atualizacaoService)
    {
        _atualizacaoService = atualizacaoService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var atualizacao = await _atualizacaoService.ObterPorIdAsync(id);
        if (atualizacao == null)
            return NotFound();

        AtualizacaoId = id;
        Atualizacao = new AtualizacaoDTO
        {
            Id = atualizacao.Id,
            Versao = atualizacao.Versao,
            Conteudo = atualizacao.Conteudo
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            AtualizacaoId = id;
            return Page();
        }

        var resultado = await _atualizacaoService.AtualizarAsync(id, Atualizacao);

        if (!resultado.Sucesso)
        {
            ErrorMessage = resultado.Mensagem;
            AtualizacaoId = id;
            return Page();
        }

        return RedirectToPage("/Admin/Atualizacoes/Index");
    }
}
