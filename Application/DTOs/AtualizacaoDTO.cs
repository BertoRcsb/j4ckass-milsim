using System.ComponentModel.DataAnnotations;

namespace GrupoArmaReforger.Application.DTOs;

/// <summary>
/// DTO para criar/editar uma atualização
/// </summary>
public class AtualizacaoDTO
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Versão é obrigatória")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Versão deve ter entre 3 e 50 caracteres")]
    public string Versao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Conteúdo é obrigatório")]
    [StringLength(5000, MinimumLength = 10, ErrorMessage = "Conteúdo deve ter entre 10 e 5000 caracteres")]
    public string Conteudo { get; set; } = string.Empty;
}

/// <summary>
/// DTO para exibir uma atualização (leitura)
/// </summary>
public class AtualizacaoExibicaoDTO
{
    public int Id { get; set; }

    public string Versao { get; set; } = string.Empty;

    public string Conteudo { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; }

    public DateTime DataAtualizacao { get; set; }

    public string AutorEmail { get; set; } = string.Empty;
}

/// <summary>
/// DTO para resultado de operações com atualizações
/// </summary>
public class AtualizacaoResultadoDTO
{
    public bool Sucesso { get; set; }

    public string Mensagem { get; set; } = string.Empty;

    public int? AtualizacaoId { get; set; }

    public AtualizacaoResultadoDTO() { }

    public AtualizacaoResultadoDTO(bool sucesso, string mensagem, int? atualizacaoId = null)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        AtualizacaoId = atualizacaoId;
    }
}
