using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Pages;

public class RecrutamentoModel : PageModel
{
    private readonly IRecrutamentoService _recrutamentoService;

    [BindProperty]
    public string? Nome { get; set; }

    [BindProperty]
    public string? Email { get; set; }

    [BindProperty]
    public string? SteamID { get; set; }

    [BindProperty]
    public string? PSN { get; set; }

    public RecrutamentoResultadoDTO? Resultado { get; private set; }

    public RecrutamentoModel(IRecrutamentoService recrutamentoService)
    {
        _recrutamentoService = recrutamentoService;
    }

    public void OnGet()
    {
    }

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
