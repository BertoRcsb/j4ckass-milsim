using System.ComponentModel.DataAnnotations;

namespace GrupoArmaReforger.Application.DTOs;

/// <summary>
/// DTO para criar/editar um aviso
/// </summary>
public class AvisoDTO
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Conteúdo é obrigatório")]
    [StringLength(5000, MinimumLength = 10, ErrorMessage = "Conteúdo deve ter entre 10 e 5000 caracteres")]
    public string Conteudo { get; set; } = string.Empty;
}

/// <summary>
/// DTO para exibir um aviso (leitura)
/// </summary>
public class AvisoExibicaoDTO
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Conteudo { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; }

    public DateTime DataAtualizacao { get; set; }

    public string AutorEmail { get; set; } = string.Empty;
}

/// <summary>
/// DTO para resultado de operações com avisos
/// </summary>
public class AvisoResultadoDTO
{
    public bool Sucesso { get; set; }

    public string Mensagem { get; set; } = string.Empty;

    public int? AvisoId { get; set; }

    public AvisoResultadoDTO() { }

    public AvisoResultadoDTO(bool sucesso, string mensagem, int? avisoId = null)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        AvisoId = avisoId;
    }
}
