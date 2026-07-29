namespace GrupoArmaReforger.Core.Domain;

public class Operador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? SteamID { get; set; }
    public string? PSN { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("Nome é obrigatório", nameof(Nome));

        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("Email é obrigatório", nameof(Email));

        if (!Email.Contains("@"))
            throw new ArgumentException("Email inválido", nameof(Email));

        if (string.IsNullOrWhiteSpace(SteamID) && string.IsNullOrWhiteSpace(PSN))
            throw new ArgumentException("SteamID ou PSN é obrigatório", nameof(SteamID));
    }
}
