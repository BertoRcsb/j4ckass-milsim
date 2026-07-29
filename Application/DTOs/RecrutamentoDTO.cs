using System.ComponentModel.DataAnnotations;
using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Application.DTOs;

/// <summary>
/// DTO para recebimento de dados de recrutamento do usuário
/// Inclui validações de apresentação
/// </summary>
public class RecrutamentoDTO
{
    /// <summary>
    /// Nome completo do candidato
    /// </summary>
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(
        AppConstants.Validation.MaxNomeLength,
        MinimumLength = 3,
        ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Email do candidato para contato
    /// </summary>
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(AppConstants.Validation.MaxEmailLength)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// ID da Steam (opcional se PSN preenchido)
    /// </summary>
    [StringLength(AppConstants.Validation.MaxSteamIdLength)]
    public string? SteamID { get; set; }

    /// <summary>
    /// PlayStation Network ID (opcional se SteamID preenchido)
    /// </summary>
    [StringLength(AppConstants.Validation.MaxPsnLength)]
    public string? PSN { get; set; }
}

/// <summary>
/// DTO para resposta de operação de recrutamento
/// Comunica sucesso/erro e resultado para o cliente
/// </summary>
public class RecrutamentoResultadoDTO
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Sucesso { get; set; }

    /// <summary>
    /// Mensagem amigável para o usuário (sucesso ou erro)
    /// </summary>
    public string Mensagem { get; set; } = string.Empty;

    /// <summary>
    /// ID do operador criado (preenchido apenas em sucesso)
    /// </summary>
    public int? OperadorId { get; set; }
}
