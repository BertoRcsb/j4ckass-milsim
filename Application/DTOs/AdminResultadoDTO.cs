namespace GrupoArmaReforger.Application.DTOs;

/// <summary>
/// DTO para resultado de operações de administração
/// </summary>
public class AdminResultadoDTO
{
    public bool Sucesso { get; set; }

    public string Mensagem { get; set; } = string.Empty;

    public int? AdminId { get; set; }

    public AdminResultadoDTO() { }

    public AdminResultadoDTO(bool sucesso, string mensagem, int? adminId = null)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        AdminId = adminId;
    }
}
