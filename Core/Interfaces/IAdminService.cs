using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Contrato para serviços de autenticação e gerenciamento de admins
/// </summary>
public interface IAdminService
{
    /// <summary>
    /// Autentica um administrador com email e senha
    /// </summary>
    /// <returns>Resultado com ID do admin se sucesso, mensagem de erro caso contrário</returns>
    Task<AdminResultadoDTO> AutenticarAsync(string email, string senha);

    /// <summary>
    /// Cria um novo administrador durante o setup inicial
    /// </summary>
    Task<AdminResultadoDTO> CriarAdminAsync(string email, string senha);

    /// <summary>
    /// Registra o login de um admin
    /// </summary>
    Task<bool> RegistrarLoginAsync(int adminId);

    /// <summary>
    /// Verifica se existe algum admin cadastrado
    /// </summary>
    Task<bool> ExisteAdminAsync();
}
