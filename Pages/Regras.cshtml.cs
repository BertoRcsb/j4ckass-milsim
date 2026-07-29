using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Pages;

/// <summary>
/// PageModel para página de Regras da comunidade
/// </summary>
public class RegrasModel : PageModel
{
    /// <summary>
    /// URL do Discord da comunidade
    /// </summary>
    public string DiscordUrl { get; } = AppConstants.Links.DiscordUrl;

    public void OnGet()
    {
    }
}
