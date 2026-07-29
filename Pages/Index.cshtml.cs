using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Core.Interfaces;

namespace GrupoArmaReforger.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly IAssetService _assetService;

    public string DiscordUrl { get; private set; } = "https://discord.gg/j4ckass";
    public string LogoUrl { get; private set; } = string.Empty;
    public string HeroBackgroundUrl { get; private set; } = string.Empty;

    public IndexModel(IConfiguration configuration, IAssetService assetService)
    {
        _configuration = configuration;
        _assetService = assetService;
    }

    public void OnGet()
    {
        DiscordUrl = _configuration["Community:DiscordUrl"] ?? "https://discord.gg/j4ckass";
        LogoUrl = _assetService.GetLogoUrl("j4novo.png");
        HeroBackgroundUrl = _assetService.GetWallpaperUrl("operations-hero.jpg");
    }
}
