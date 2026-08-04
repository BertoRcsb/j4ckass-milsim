using Moq;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.Services;
using GrupoArmaReforger.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace GrupoArmaReforger.Tests;

/// <summary>
/// Testes unitários para AvisoService
/// </summary>
[TestClass]
public class AvisoServiceTests
{
    private Mock<IAvisoRepository> _mockRepository = null!;
    private Mock<ILogger<AvisoService>> _mockLogger = null!;
    private AvisoService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IAvisoRepository>();
        _mockLogger = new Mock<ILogger<AvisoService>>();
        _service = new AvisoService(_mockRepository.Object, _mockLogger.Object);
    }

    [TestMethod]
    public async Task CriarAsync_ComDadosValidos_RetornaSucesso()
    {
        // Arrange
        var dto = new AvisoDTO
        {
            Titulo = "Aviso de Teste",
            Conteudo = "Este é um aviso de teste com conteúdo válido"
        };

        _mockRepository.Setup(r => r.AdicionarAsync(It.IsAny<Aviso>()))
            .ReturnsAsync(new Aviso
            {
                Id = 1,
                Titulo = dto.Titulo,
                Conteudo = dto.Conteudo,
                AdminUserId = 1
            });

        // Act
        var resultado = await _service.CriarAsync(dto, 1);

        // Assert
        Assert.IsTrue(resultado.Sucesso);
        Assert.AreEqual(1, resultado.AvisoId);
        _mockRepository.Verify(r => r.AdicionarAsync(It.IsAny<Aviso>()), Times.Once);
    }

    [TestMethod]
    public async Task CriarAsync_ComTituloVazio_RetornaErro()
    {
        // Arrange
        var dto = new AvisoDTO
        {
            Titulo = string.Empty,
            Conteudo = "Conteúdo válido"
        };

        // Act
        var resultado = await _service.CriarAsync(dto, 1);

        // Assert
        Assert.IsFalse(resultado.Sucesso);
    }

    [TestMethod]
    public async Task ObterTodosAsync_RetornaListaDeAvisos()
    {
        // Arrange
        var avisos = new List<Aviso>
        {
            new Aviso { Id = 1, Titulo = "Aviso 1", Conteudo = "Conteúdo 1", AdminUserId = 1 },
            new Aviso { Id = 2, Titulo = "Aviso 2", Conteudo = "Conteúdo 2", AdminUserId = 1 }
        };

        _mockRepository.Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(avisos);

        // Act
        var resultado = await _service.ObterTodosAsync();

        // Assert
        Assert.AreEqual(2, resultado.Count());
        _mockRepository.Verify(r => r.ObterTodosAsync(), Times.Once);
    }

    [TestMethod]
    public async Task RemoverAsync_ComIdValido_RetornaSucesso()
    {
        // Arrange
        var id = 1;

        _mockRepository.Setup(r => r.RemoverAsync(id))
            .ReturnsAsync(true);

        // Act
        var resultado = await _service.RemoverAsync(id);

        // Assert
        Assert.IsTrue(resultado.Sucesso);
        _mockRepository.Verify(r => r.RemoverAsync(id), Times.Once);
    }
}
