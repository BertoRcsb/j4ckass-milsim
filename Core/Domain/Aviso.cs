using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Core.Domain;

/// <summary>
/// Representa um aviso/comunicado publicado para a comunidade
/// </summary>
public class Aviso
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

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
        ValidarTitulo();
        ValidarConteudo();
    }

    private void ValidarTitulo()
    {
        if (string.IsNullOrWhiteSpace(Titulo))
            throw new ArgumentException(AppConstants.Admin.Aviso.Validacao.TituloObrigatorio, nameof(Titulo));

        if (Titulo.Length > AppConstants.Validation.MaxTituloLength)
            throw new ArgumentException($"Título não pode exceder {AppConstants.Validation.MaxTituloLength} caracteres", nameof(Titulo));
    }

    private void ValidarConteudo()
    {
        if (string.IsNullOrWhiteSpace(Conteudo))
            throw new ArgumentException(AppConstants.Admin.Aviso.Validacao.ConteudoObrigatorio, nameof(Conteudo));

        if (Conteudo.Length > AppConstants.Validation.MaxConteudoLength)
            throw new ArgumentException($"Conteúdo não pode exceder {AppConstants.Validation.MaxConteudoLength} caracteres", nameof(Conteudo));
    }
}
