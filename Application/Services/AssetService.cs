using GrupoArmaReforger.Core.Interfaces;

namespace GrupoArmaReforger.Application.Services;

public class AssetService : IAssetService
{
    private const string LogosPath = "/assets/logos";
    private const string WallpapersPath = "/assets/wallpapers";
    private const string HerosPath = "/assets/heroes";

    public string GetLogoUrl(string logoName) =>
        $"{LogosPath}/{logoName}";

    public string GetWallpaperUrl(string wallpaperName) =>
        $"{WallpapersPath}/{wallpaperName}";

    public string GetHeroBackgroundUrl(string heroName) =>
        $"{HerosPath}/{heroName}";
}
