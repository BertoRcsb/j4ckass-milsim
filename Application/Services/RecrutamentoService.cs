using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Application.Services;

public class RecrutamentoService : IRecrutamentoService
{
    private readonly IOperadorRepository _repository;
    private readonly ILogger<RecrutamentoService> _logger;

    public RecrutamentoService(IOperadorRepository repository, ILogger<RecrutamentoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(RecrutamentoDTO recrutamento)
    {
        try
        {
            var operador = new Operador
            {
                Nome = recrutamento.Nome.Trim(),
                Email = recrutamento.Email.Trim().ToLowerInvariant(),
                SteamID = recrutamento.SteamID?.Trim(),
                PSN = recrutamento.PSN?.Trim()
            };

            operador.Validar();

            if (await _repository.ExisteEmailAsync(operador.Email))
            {
                return new RecrutamentoResultadoDTO
                {
                    Sucesso = false,
                    Mensagem = "Este email já foi cadastrado."
                };
            }

            var operadorCriado = await _repository.AdicionarAsync(operador);

            _logger.LogInformation($"Novo recrutamento cadastrado: {operadorCriado.Nome} ({operadorCriado.Email})");

            return new RecrutamentoResultadoDTO
            {
                Sucesso = true,
                Mensagem = "Recrutamento realizado com sucesso! Aguarde contato da staff.",
                OperadorId = operadorCriado.Id
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning($"Validação falhou: {ex.Message}");
            return new RecrutamentoResultadoDTO
            {
                Sucesso = false,
                Mensagem = ex.Message
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao cadastrar recrutamento: {ex.Message}");
            return new RecrutamentoResultadoDTO
            {
                Sucesso = false,
                Mensagem = "Erro ao processar recrutamento. Tente novamente mais tarde."
            };
        }
    }

    public async Task<IEnumerable<RecrutamentoDTO>> ObterRecrutasAsync()
    {
        var operadores = await _repository.ObterTodosAsync();

        return operadores.Select(o => new RecrutamentoDTO
        {
            Nome = o.Nome,
            Email = o.Email,
            SteamID = o.SteamID,
            PSN = o.PSN
        });
    }
}
