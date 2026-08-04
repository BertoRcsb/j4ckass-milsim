using Microsoft.Extensions.Logging;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Core.Constants;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Application.Services;

/// <summary>
/// Serviço para gerenciamento de atualizações
/// </summary>
public class AtualizacaoService
{
    private readonly IAtualizacaoRepository _repository;
    private readonly ILogger<AtualizacaoService> _logger;

    public AtualizacaoService(IAtualizacaoRepository repository, ILogger<AtualizacaoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Obtém todas as atualizações públicas
    /// </summary>
    public async Task<IEnumerable<AtualizacaoExibicaoDTO>> ObterTodosAsync()
    {
        try
        {
            var atualizacoes = await _repository.ObterTodosAsync();
            return MapearParaExibicao(atualizacoes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter atualizações");
            return Enumerable.Empty<AtualizacaoExibicaoDTO>();
        }
    }

    /// <summary>
    /// Obtém uma atualização pelo ID
    /// </summary>
    public async Task<AtualizacaoExibicaoDTO?> ObterPorIdAsync(int id)
    {
        try
        {
            var atualizacao = await _repository.ObterPorIdAsync(id);
            return atualizacao == null ? null : MapearParaExibicao(atualizacao);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter atualização ID: {Id}", id);
            return null;
        }
    }

    /// <summary>
    /// Cria uma nova atualização
    /// </summary>
    public async Task<AtualizacaoResultadoDTO> CriarAsync(AtualizacaoDTO dto, int adminId)
    {
        try
        {
            var atualizacao = new Atualizacao
            {
                Versao = dto.Versao,
                Conteudo = dto.Conteudo,
                AdminUserId = adminId,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            };

            atualizacao.Validar();
            await _repository.AdicionarAsync(atualizacao);

            _logger.LogInformation("Atualização criada com sucesso: {Versao} por Admin ID: {AdminId}", atualizacao.Versao, adminId);
            return new AtualizacaoResultadoDTO(true, AppConstants.Admin.Atualizacao.CriacaoSucesso, atualizacao.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao criar atualização: {Mensagem}", ex.Message);
            return new AtualizacaoResultadoDTO(false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar atualização");
            return new AtualizacaoResultadoDTO(false, AppConstants.Admin.Atualizacao.ErroProcessamento);
        }
    }

    /// <summary>
    /// Atualiza uma atualização existente
    /// </summary>
    public async Task<AtualizacaoResultadoDTO> AtualizarAsync(int id, AtualizacaoDTO dto)
    {
        try
        {
            var atualizacao = await _repository.ObterPorIdAsync(id);
            if (atualizacao == null)
                return new AtualizacaoResultadoDTO(false, "Atualização não encontrada");

            atualizacao.Versao = dto.Versao;
            atualizacao.Conteudo = dto.Conteudo;
            atualizacao.DataAtualizacao = DateTime.UtcNow;

            atualizacao.Validar();
            await _repository.AtualizarAsync(atualizacao);

            _logger.LogInformation("Atualização atualizada com sucesso: ID {Id}", id);
            return new AtualizacaoResultadoDTO(true, AppConstants.Admin.Atualizacao.EdicaoSucesso, atualizacao.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Erro de validação ao atualizar atualização: {Mensagem}", ex.Message);
            return new AtualizacaoResultadoDTO(false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar atualização ID: {Id}", id);
            return new AtualizacaoResultadoDTO(false, AppConstants.Admin.Atualizacao.ErroProcessamento);
        }
    }

    /// <summary>
    /// Remove uma atualização
    /// </summary>
    public async Task<AtualizacaoResultadoDTO> RemoverAsync(int id)
    {
        try
        {
            var sucesso = await _repository.RemoverAsync(id);
            if (!sucesso)
                return new AtualizacaoResultadoDTO(false, "Atualização não encontrada");

            _logger.LogInformation("Atualização removida com sucesso: ID {Id}", id);
            return new AtualizacaoResultadoDTO(true, AppConstants.Admin.Atualizacao.DelecaoSucesso);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover atualização ID: {Id}", id);
            return new AtualizacaoResultadoDTO(false, AppConstants.Admin.Atualizacao.ErroProcessamento);
        }
    }

    private static IEnumerable<AtualizacaoExibicaoDTO> MapearParaExibicao(IEnumerable<Atualizacao> atualizacoes)
    {
        return atualizacoes.Select(a => new AtualizacaoExibicaoDTO
        {
            Id = a.Id,
            Versao = a.Versao,
            Conteudo = a.Conteudo,
            DataCriacao = a.DataCriacao,
            DataAtualizacao = a.DataAtualizacao,
            AutorEmail = a.AdminUser?.Email ?? "Sistema"
        });
    }

    private static AtualizacaoExibicaoDTO MapearParaExibicao(Atualizacao atualizacao)
    {
        return new AtualizacaoExibicaoDTO
        {
            Id = atualizacao.Id,
            Versao = atualizacao.Versao,
            Conteudo = atualizacao.Conteudo,
            DataCriacao = atualizacao.DataCriacao,
            DataAtualizacao = atualizacao.DataAtualizacao,
            AutorEmail = atualizacao.AdminUser?.Email ?? "Sistema"
        };
    }
}
