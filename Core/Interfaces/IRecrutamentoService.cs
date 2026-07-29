using GrupoArmaReforger.Application.DTOs;

namespace GrupoArmaReforger.Core.Interfaces;

public interface IRecrutamentoService
{
    Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(RecrutamentoDTO recrutamento);
    Task<IEnumerable<RecrutamentoDTO>> ObterRecrutasAsync();
}
