using System.ComponentModel.DataAnnotations;

namespace GrupoArmaReforger.Application.DTOs;

/// <summary>
/// DTO para credenciais de login de administrador
/// </summary>
public class LoginDTO
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
    public string Senha { get; set; } = string.Empty;
}
