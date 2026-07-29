using GrupoArmaReforger.Core.Interfaces;

namespace GrupoArmaReforger.Application.Services;

/// <summary>
/// Serviço para gerenciar URLs de assets
/// Centraliza a lógica de construção de caminhos para imagens
/// </summary>
public class AssetService : IAssetService
{
    private const string LogosPath = "/assets/logos";
    private const string WallpapersPath = "/assets/wallpapers";
    private const string HerosPath = "/assets/heroes";

    /// <summary>
    /// Obtém a URL relativa de uma logo
    /// </summary>
    /// <param name="logoName">Nome do arquivo (ex: "jckss.png")</param>
    /// <returns>URL relativa completa da logo</returns>
    public string GetLogoUrl(string logoName)
    {
        ValidarNomeArquivo(logoName);
        return ConstructUrl(LogosPath, logoName);
    }

    /// <summary>
    /// Obtém a URL relativa de um wallpaper
    /// </summary>
    /// <param name="wallpaperName">Nome do arquivo (ex: "operations-hero.jpg")</param>
    /// <returns>URL relativa completa do wallpaper</returns>
    public string GetWallpaperUrl(string wallpaperName)
    {
        ValidarNomeArquivo(wallpaperName);
        return ConstructUrl(WallpapersPath, wallpaperName);
    }

    /// <summary>
    /// Obtém a URL relativa de um hero background
    /// </summary>
    /// <param name="heroName">Nome do arquivo (ex: "tactical-background.svg")</param>
    /// <returns>URL relativa completa do hero</returns>
    public string GetHeroBackgroundUrl(string heroName)
    {
        ValidarNomeArquivo(heroName);
        return ConstructUrl(HerosPath, heroName);
    }

    /// <summary>
    /// Constrói a URL combinando caminho e nome do arquivo
    /// </summary>
    private static string ConstructUrl(string basePath, string fileName) =>
        $"{basePath}/{fileName}";

    /// <summary>
    /// Valida se o nome do arquivo não é nulo ou vazio
    /// </summary>
    /// <exception cref="ArgumentException">Se o nome for inválido</exception>
    private static void ValidarNomeArquivo(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Nome do arquivo não pode ser vazio.", nameof(fileName));
    }
}
