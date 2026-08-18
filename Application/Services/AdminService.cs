using Microsoft.Extensions.Logging;
using BCrypt.Net;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Core.Constants;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Application.Services;

/// <summary>
/// Serviço para autenticação e gerenciamento de administradores
/// </summary>
public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IAdminRepository repository, ILogger<AdminService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Autentica um administrador com email e senha
    /// </summary>
    public async Task<AdminResultadoDTO> AutenticarAsync(string email, string senha)
    {
        try
        {
            ValidarCredenciais(email, senha);

            var admin = await _repository.ObterPorEmailAsync(email);
            if (admin == null)
            {
                _logger.LogWarning("Tentativa de login com email não registrado: {Email}", email);
                return new AdminResultadoDTO(false, AppConstants.Admin.LoginFalha);
            }

            if (!admin.Ativo)
            {
                _logger.LogWarning("Tentativa de login com admin inativo: {Email}", email);
                return new AdminResultadoDTO(false, AppConstants.Admin.LoginFalha);
            }

            if (!VerificarSenha(senha, admin.SenhaHash))
            {
                _logger.LogWarning("Senha incorreta para admin: {Email}", email);
                return new AdminResultadoDTO(false, AppConstants.Admin.LoginFalha);
            }

            await RegistrarLoginAsync(admin.Id);

            _logger.LogInformation("Admin autenticado com sucesso: {Email}", email);
            return new AdminResultadoDTO(true, AppConstants.Admin.LoginSucesso, admin.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação no login: {Mensagem}", ex.Message);
            return new AdminResultadoDTO(false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao autenticar admin com email: {Email}", email);
            return new AdminResultadoDTO(false, AppConstants.Admin.ErroProcessamento);
        }
    }

    /// <summary>
    /// Cria um novo administrador durante o setup inicial
    /// </summary>
    public async Task<AdminResultadoDTO> CriarAdminAsync(string email, string senha)
    {
        try
        {
            ValidarCredenciais(email, senha);

            if (await _repository.ExisteEmailAsync(email))
            {
                _logger.LogWarning("Tentativa de criar admin com email duplicado: {Email}", email);
                return new AdminResultadoDTO(false, AppConstants.Admin.AdminUser.EmailJaCadastrado);
            }

            var admin = new AdminUser
            {
                Email = email,
                SenhaHash = HashSenha(senha),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            admin.Validar();
            await _repository.AdicionarAsync(admin);

            _logger.LogInformation("Admin criado com sucesso: {Email}", email);
            return new AdminResultadoDTO(true, "Administrador criado com sucesso", admin.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao criar admin: {Mensagem}", ex.Message);
            return new AdminResultadoDTO(false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar admin com email: {Email}", email);
            return new AdminResultadoDTO(false, AppConstants.Admin.ErroProcessamento);
        }
    }

    /// <summary>
    /// Registra o login de um admin
    /// </summary>
    public async Task<bool> RegistrarLoginAsync(int adminId)
    {
        try
        {
            var admin = await _repository.ObterPorIdAsync(adminId);
            if (admin == null)
                return false;

            admin.RegistrarLogin();
            await _repository.AtualizarAsync(admin);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar login para admin ID: {AdminId}", adminId);
            return false;
        }
    }

    /// <summary>
    /// Verifica se existe algum admin cadastrado
    /// </summary>
    public async Task<bool> ExisteAdminAsync()
    {
        try
        {
            return await _repository.ObterPorIdAsync(1) != null;
        }
        catch
        {
            return false;
        }
    }

    private static void ValidarCredenciais(string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(AppConstants.Admin.AdminUser.Validacao.EmailObrigatorio);

        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException(AppConstants.Admin.AdminUser.Validacao.SenhaObrigatoria);

        if (senha.Length < AppConstants.Validation.MinSenhaLength)
            throw new ArgumentException(AppConstants.Admin.AdminUser.Validacao.SenhaFraca);
    }

    private static string HashSenha(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12);
    }

    private static bool VerificarSenha(string senha, string hash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
        catch
        {
            return false;
        }
    }
}
