namespace GrupoArmaReforger.Core.Interfaces;

public interface IAssetService
{
    string GetLogoUrl(string logoName);
    string GetWallpaperUrl(string wallpaperName);
    string GetHeroBackgroundUrl(string heroName);
}
