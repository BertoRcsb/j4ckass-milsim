using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Pages;

/// <summary>
/// PageModel para página de Recrutamento
/// Orquestra o envio de formulário de recrutamento
/// </summary>
public class RecrutamentoModel : PageModel
{
    private readonly IRecrutamentoService _recrutamentoService;

    /// <summary>
    /// Nome do candidato (dados do formulário)
    /// </summary>
    [BindProperty]
    public string? Nome { get; set; }

    /// <summary>
    /// Email do candidato (dados do formulário)
    /// </summary>
    [BindProperty]
    public string? Email { get; set; }

    /// <summary>
    /// Steam ID do candidato (dados do formulário)
    /// </summary>
    [BindProperty]
    public string? SteamID { get; set; }

    /// <summary>
    /// PSN do candidato (dados do formulário)
    /// </summary>
    [BindProperty]
    public string? PSN { get; set; }

    /// <summary>
    /// Resultado da operação de recrutamento
    /// Null enquanto não há resultado; preenchido após POST
    /// </summary>
    public RecrutamentoResultadoDTO? Resultado { get; private set; }

    public RecrutamentoModel(IRecrutamentoService recrutamentoService)
    {
        _recrutamentoService = recrutamentoService;
    }

    /// <summary>
    /// Carrega página de recrutamento
    /// </summary>
    public void OnGet()
    {
    }

    /// <summary>
    /// Processa envio do formulário de recrutamento
    /// </summary>
    public async Task OnPostAsync()
    {
        var recrutamento = new RecrutamentoDTO
        {
            Nome = Nome ?? string.Empty,
            Email = Email ?? string.Empty,
            SteamID = SteamID,
            PSN = PSN
        };

        Resultado = await _recrutamentoService.CadastrarRecrutaAsync(recrutamento);
    }
}
