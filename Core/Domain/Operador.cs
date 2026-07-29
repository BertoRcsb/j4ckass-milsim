using System.Text.RegularExpressions;
using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Core.Domain;

/// <summary>
/// Representa um operador cadastrado no sistema de recrutamento
/// </summary>
public class Operador
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? SteamID { get; set; }

    public string? PSN { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Valida a entidade conforme regras de negócio
    /// </summary>
    /// <exception cref="ArgumentException">Lançada se alguma validação falhar</exception>
    public void Validar()
    {
        ValidarNome();
        ValidarEmail();
        ValidarPlataforma();
    }

    private void ValidarNome()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException(AppConstants.Messages.Recrutamento.Validacao.NomeObrigatorio, nameof(Nome));

        if (Nome.Length > AppConstants.Validation.MaxNomeLength)
            throw new ArgumentException($"Nome não pode exceder {AppConstants.Validation.MaxNomeLength} caracteres", nameof(Nome));
    }

    private void ValidarEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException(AppConstants.Messages.Recrutamento.Validacao.EmailObrigatorio, nameof(Email));

        if (!IsEmailValido(Email))
            throw new ArgumentException(AppConstants.Messages.Recrutamento.Validacao.EmailInvalido, nameof(Email));

        if (Email.Length > AppConstants.Validation.MaxEmailLength)
            throw new ArgumentException($"Email não pode exceder {AppConstants.Validation.MaxEmailLength} caracteres", nameof(Email));
    }

    private void ValidarPlataforma()
    {
        bool temSteamId = !string.IsNullOrWhiteSpace(SteamID);
        bool temPsn = !string.IsNullOrWhiteSpace(PSN);

        if (!temSteamId && !temPsn)
            throw new ArgumentException(AppConstants.Messages.Recrutamento.Validacao.PlataformaObrigatoria, nameof(SteamID));
    }

    /// <summary>
    /// Valida se o email está em formato correto usando Regex
    /// </summary>
    private static bool IsEmailValido(string email)
    {
        try
        {
            return Regex.IsMatch(email, AppConstants.Validation.EmailRegex, RegexOptions.IgnoreCase);
        }
        catch
        {
            return false;
        }
    }
}
