using Microsoft.Extensions.Logging;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Core.Constants;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Application.Services;

/// <summary>
/// Serviço para gerenciamento de avisos
/// </summary>
public class AvisoService
{
    private readonly IAvisoRepository _repository;
    private readonly ILogger<AvisoService> _logger;

    public AvisoService(IAvisoRepository repository, ILogger<AvisoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Obtém todos os avisos públicos
    /// </summary>
    public async Task<IEnumerable<AvisoExibicaoDTO>> ObterTodosAsync()
    {
        try
        {
            var avisos = await _repository.ObterTodosAsync();
            return MapearParaExibicao(avisos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter avisos");
            return Enumerable.Empty<AvisoExibicaoDTO>();
        }
    }

    /// <summary>
    /// Obtém um aviso pelo ID
    /// </summary>
    public async Task<AvisoExibicaoDTO?> ObterPorIdAsync(int id)
    {
        try
        {
            var aviso = await _repository.ObterPorIdAsync(id);
            return aviso == null ? null : MapearParaExibicao(aviso);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter aviso ID: {Id}", id);
            return null;
        }
    }

    /// <summary>
    /// Cria um novo aviso
    /// </summary>
    public async Task<AvisoResultadoDTO> CriarAsync(AvisoDTO dto, int adminId)
    {
        try
        {
            var aviso = new Aviso
            {
                Titulo = dto.Titulo,
                Conteudo = dto.Conteudo,
                AdminUserId = adminId,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            };

            aviso.Validar();
            await _repository.AdicionarAsync(aviso);

            _logger.LogInformation("Aviso criado com sucesso: {Titulo} por Admin ID: {AdminId}", aviso.Titulo, adminId);
            return new AvisoResultadoDTO(true, AppConstants.Admin.Aviso.CriacaoSucesso, aviso.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao criar aviso: {Mensagem}", ex.Message);
            return new AvisoResultadoDTO(false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar aviso");
            return new AvisoResultadoDTO(false, AppConstants.Admin.Aviso.ErroProcessamento);
        }
    }

    /// <summary>
    /// Atualiza um aviso existente
    /// </summary>
    public async Task<AvisoResultadoDTO> AtualizarAsync(int id, AvisoDTO dto)
    {
        try
        {
            var aviso = await _repository.ObterPorIdAsync(id);
            if (aviso == null)
                return new AvisoResultadoDTO(false, "Aviso não encontrado");

            aviso.Titulo = dto.Titulo;
            aviso.Conteudo = dto.Conteudo;
            aviso.DataAtualizacao = DateTime.UtcNow;

            aviso.Validar();
            await _repository.AtualizarAsync(aviso);

            _logger.LogInformation("Aviso atualizado com sucesso: ID {Id}", id);
            return new AvisoResultadoDTO(true, AppConstants.Admin.Aviso.EdicaoSucesso, aviso.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao atualizar aviso: {Mensagem}", ex.Message);
            return new AvisoResultadoDTO(false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar aviso ID: {Id}", id);
            return new AvisoResultadoDTO(false, AppConstants.Admin.Aviso.ErroProcessamento);
        }
    }

    /// <summary>
    /// Remove um aviso
    /// </summary>
    public async Task<AvisoResultadoDTO> RemoverAsync(int id)
    {
        try
        {
            var sucesso = await _repository.RemoverAsync(id);
            if (!sucesso)
                return new AvisoResultadoDTO(false, "Aviso não encontrado");

            _logger.LogInformation("Aviso removido com sucesso: ID {Id}", id);
            return new AvisoResultadoDTO(true, AppConstants.Admin.Aviso.DelecaoSucesso);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover aviso ID: {Id}", id);
            return new AvisoResultadoDTO(false, AppConstants.Admin.Aviso.ErroProcessamento);
        }
    }

    private static IEnumerable<AvisoExibicaoDTO> MapearParaExibicao(IEnumerable<Aviso> avisos)
    {
        return avisos.Select(a => new AvisoExibicaoDTO
        {
            Id = a.Id,
            Titulo = a.Titulo,
            Conteudo = a.Conteudo,
            DataCriacao = a.DataCriacao,
            DataAtualizacao = a.DataAtualizacao,
            AutorEmail = a.AdminUser?.Email ?? "Sistema"
        });
    }

    private static AvisoExibicaoDTO MapearParaExibicao(Aviso aviso)
    {
        return new AvisoExibicaoDTO
        {
            Id = aviso.Id,
            Titulo = aviso.Titulo,
            Conteudo = aviso.Conteudo,
            DataCriacao = aviso.DataCriacao,
            DataAtualizacao = aviso.DataAtualizacao,
            AutorEmail = aviso.AdminUser?.Email ?? "Sistema"
        };
    }
}
