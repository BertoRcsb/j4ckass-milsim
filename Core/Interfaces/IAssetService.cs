namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Define o contrato para gerenciamento de URLs de assets
/// </summary>
public interface IAssetService
{
    /// <summary>
    /// Obtém a URL de uma logo
    /// </summary>
    /// <param name="logoName">Nome do arquivo da logo</param>
    /// <returns>URL relativa da logo</returns>
    string GetLogoUrl(string logoName);

    /// <summary>
    /// Obtém a URL de um wallpaper
    /// </summary>
    /// <param name="wallpaperName">Nome do arquivo do wallpaper</param>
    /// <returns>URL relativa do wallpaper</returns>
    string GetWallpaperUrl(string wallpaperName);
}
