using System.Text.RegularExpressions;
using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Core.Domain;

/// <summary>
/// Representa um usuário administrador do sistema
/// </summary>
public class AdminUser
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public DateTime? DataUltimoLogin { get; set; }

    public ICollection<Aviso> Avisos { get; set; } = new List<Aviso>();

    public ICollection<Atualizacao> Atualizacoes { get; set; } = new List<Atualizacao>();

    /// <summary>
    /// Valida a entidade conforme regras de negócio
    /// </summary>
    /// <exception cref="ArgumentException">Lançada se alguma validação falhar</exception>
    public void Validar()
    {
        ValidarEmail();
    }

    private void ValidarEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException(AppConstants.Admin.AdminUser.Validacao.EmailObrigatorio, nameof(Email));

        if (!IsEmailValido(Email))
            throw new ArgumentException(AppConstants.Messages.Recrutamento.Validacao.EmailInvalido, nameof(Email));

        if (Email.Length > AppConstants.Validation.MaxEmailLength)
            throw new ArgumentException($"Email não pode exceder {AppConstants.Validation.MaxEmailLength} caracteres", nameof(Email));
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

    /// <summary>
    /// Registra a data do último login do administrador
    /// </summary>
    public void RegistrarLogin()
    {
        DataUltimoLogin = DateTime.UtcNow;
    }
}
