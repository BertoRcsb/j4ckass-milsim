using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Core.Interfaces;

/// <summary>
/// Define o contrato para operações de recrutamento
/// </summary>
public interface IRecrutamentoService
{
    /// <summary>
    /// Cadastra um novo recrutamento com validações de negócio
    /// </summary>
    /// <param name="recrutamento">Dados do recrutamento</param>
    /// <returns>Resultado com status de sucesso/erro e mensagem</returns>
    Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(RecrutamentoDTO recrutamento);

    /// <summary>
    /// Obtém lista de todos os recrutas cadastrados
    /// </summary>
    /// <returns>Coleção de recrutas</returns>
    Task<IEnumerable<RecrutamentoDTO>> ObterRecrutasAsync();
}
