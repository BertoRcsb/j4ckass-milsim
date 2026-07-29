using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Core.Constants;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Application.Services;

/// <summary>
/// Serviço de aplicação para operações de recrutamento
/// Orquestra validações de negócio e persistência
/// </summary>
public class RecrutamentoService : IRecrutamentoService
{
    private readonly IOperadorRepository _repository;
    private readonly ILogger<RecrutamentoService> _logger;

    public RecrutamentoService(IOperadorRepository repository, ILogger<RecrutamentoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Cadastra um novo recrutamento com validações de negócio
    /// </summary>
    public async Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(RecrutamentoDTO recrutamento)
    {
        try
        {
            var operador = MapearDtoParaDominio(recrutamento);
            ValidarOperador(operador);

            var emailJaExiste = await VerificarDuplicataAsync(operador.Email);
            if (emailJaExiste)
                return CriarResultadoErro(AppConstants.Messages.Recrutamento.EmailDuplicado);

            var operadorCriado = await _repository.AdicionarAsync(operador);
            LogarSucessoCadastro(operadorCriado);

            return CriarResultadoSucesso(operadorCriado.Id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Validação de recrutamento falhou: {@Erro}", ex.Message);
            return CriarResultadoErro(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao cadastrar recrutamento");
            return CriarResultadoErro(AppConstants.Messages.Recrutamento.ErroProcessamento);
        }
    }

    /// <summary>
    /// Obtém lista de todos os recrutas cadastrados
    /// </summary>
    public async Task<IEnumerable<RecrutamentoDTO>> ObterRecrutasAsync()
    {
        var operadores = await _repository.ObterTodosAsync();
        return MapearDominioParaDto(operadores);
    }

    /// <summary>
    /// Mapeia DTO de entrada para entidade de domínio com normalização
    /// </summary>
    private static Operador MapearDtoParaDominio(RecrutamentoDTO recrutamento)
    {
        return new Operador
        {
            Nome = recrutamento.Nome.Trim(),
            Email = recrutamento.Email.Trim().ToLowerInvariant(),
            SteamID = NormalizarCampoOpcional(recrutamento.SteamID),
            PSN = NormalizarCampoOpcional(recrutamento.PSN)
        };
    }

    /// <summary>
    /// Normaliza campos opcionais (trim e null se vazio)
    /// </summary>
    private static string? NormalizarCampoOpcional(string? campo)
    {
        return string.IsNullOrWhiteSpace(campo) ? null : campo.Trim();
    }

    /// <summary>
    /// Valida a entidade conforme regras de negócio
    /// </summary>
    private static void ValidarOperador(Operador operador)
    {
        operador.Validar();
    }

    /// <summary>
    /// Verifica se um email já está cadastrado no banco
    /// </summary>
    private async Task<bool> VerificarDuplicataAsync(string email)
    {
        return await _repository.ExisteEmailAsync(email);
    }

    /// <summary>
    /// Loga o sucesso do cadastro com informações estruturadas
    /// </summary>
    private void LogarSucessoCadastro(Operador operador)
    {
        _logger.LogInformation(
            "Novo recrutamento cadastrado com sucesso: {OperadorNome} ({OperadorEmail}) - ID: {OperadorId}",
            operador.Nome,
            operador.Email,
            operador.Id);
    }

    /// <summary>
    /// Cria um resultado de sucesso
    /// </summary>
    private static RecrutamentoResultadoDTO CriarResultadoSucesso(int operadorId)
    {
        return new RecrutamentoResultadoDTO
        {
            Sucesso = true,
            Mensagem = AppConstants.Messages.Recrutamento.SucessoCadastro,
            OperadorId = operadorId
        };
    }

    /// <summary>
    /// Cria um resultado de erro com mensagem
    /// </summary>
    private static RecrutamentoResultadoDTO CriarResultadoErro(string mensagem)
    {
        return new RecrutamentoResultadoDTO
        {
            Sucesso = false,
            Mensagem = mensagem
        };
    }

    /// <summary>
    /// Mapeia entidades de domínio para DTOs
    /// </summary>
    private static IEnumerable<RecrutamentoDTO> MapearDominioParaDto(IEnumerable<Operador> operadores)
    {
        return operadores.Select(o => new RecrutamentoDTO
        {
            Nome = o.Nome,
            Email = o.Email,
            SteamID = o.SteamID,
            PSN = o.PSN
        });
    }
}
