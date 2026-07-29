using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Pages;

/// <summary>
/// PageModel para a página inicial (Home)
/// Fornece dados para renderização da home com hero section
/// </summary>
public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly IAssetService _assetService;

    /// <summary>
    /// URL do Discord da comunidade
    /// </summary>
    public string DiscordUrl { get; private set; } = string.Empty;

    /// <summary>
    /// URL relativa do logo para seção hero
    /// </summary>
    public string LogoUrl { get; private set; } = string.Empty;

    /// <summary>
    /// URL relativa do background para seção hero
    /// </summary>
    public string HeroBackgroundUrl { get; private set; } = string.Empty;

    public IndexModel(IConfiguration configuration, IAssetService assetService)
    {
        _configuration = configuration;
        _assetService = assetService;
    }

    /// <summary>
    /// Carrega dados necessários para a página
    /// </summary>
    public void OnGet()
    {
        DiscordUrl = _configuration["Community:DiscordUrl"] ?? AppConstants.Links.DiscordUrl;
        LogoUrl = _assetService.GetLogoUrl(AppConstants.Assets.LogoHome);
        HeroBackgroundUrl = _assetService.GetWallpaperUrl(AppConstants.Assets.HeroOperations);
    }
}
