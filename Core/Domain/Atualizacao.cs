using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Core.Domain;

/// <summary>
/// Representa uma atualização/versão do projeto publicada para a comunidade
/// </summary>
public class Atualizacao
{
    public int Id { get; set; }

    public string Versao { get; set; } = string.Empty;

    public string Conteudo { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

    public int AdminUserId { get; set; }

    public AdminUser? AdminUser { get; set; }

    /// <summary>
    /// Valida a entidade conforme regras de negócio
    /// </summary>
    /// <exception cref="ArgumentException">Lançada se alguma validação falhar</exception>
    public void Validar()
    {
        ValidarVersao();
        ValidarConteudo();
    }

    private void ValidarVersao()
    {
        if (string.IsNullOrWhiteSpace(Versao))
            throw new ArgumentException(AppConstants.Admin.Atualizacao.Validacao.VersaoObrigatoria, nameof(Versao));

        if (Versao.Length > AppConstants.Validation.MaxVersaoLength)
            throw new ArgumentException($"Versão não pode exceder {AppConstants.Validation.MaxVersaoLength} caracteres", nameof(Versao));
    }

    private void ValidarConteudo()
    {
        if (string.IsNullOrWhiteSpace(Conteudo))
            throw new ArgumentException(AppConstants.Admin.Atualizacao.Validacao.ConteudoObrigatorio, nameof(Conteudo));

        if (Conteudo.Length > AppConstants.Validation.MaxConteudoLength)
            throw new ArgumentException($"Conteúdo não pode exceder {AppConstants.Validation.MaxConteudoLength} caracteres", nameof(Conteudo));
    }
}
