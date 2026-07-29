namespace GrupoArmaReforger.Application.DTOs;

public class RecrutamentoDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? SteamID { get; set; }
    public string? PSN { get; set; }
}

public class RecrutamentoResultadoDTO
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public int? OperadorId { get; set; }
}
