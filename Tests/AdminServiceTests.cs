using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.Services;
using Microsoft.Extensions.Logging;

namespace GrupoArmaReforger.Tests;

/// <summary>
/// Testes unitários para AdminService
/// </summary>
[TestClass]
public class AdminServiceTests
{
    private Mock<IAdminRepository> _mockRepository = null!;
    private Mock<ILogger<AdminService>> _mockLogger = null!;
    private AdminService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IAdminRepository>();
        _mockLogger = new Mock<ILogger<AdminService>>();
        _service = new AdminService(_mockRepository.Object, _mockLogger.Object);
    }

    [TestMethod]
    public async Task AutenticarAsync_ComCredenciaisValidas_RetornaSucesso()
    {
        // Arrange
        var email = "admin@test.com";
        var senha = "SenhaSegura123";
        var admin = new AdminUser
        {
            Id = 1,
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
            Ativo = true
        };

        _mockRepository.Setup(r => r.ObterPorEmailAsync(email))
            .ReturnsAsync(admin);

        // Act
        var resultado = await _service.AutenticarAsync(email, senha);

        // Assert
        Assert.IsTrue(resultado.Sucesso);
        Assert.AreEqual(1, resultado.AdminId);
        _mockRepository.Verify(r => r.ObterPorEmailAsync(email), Times.Once);
    }

    [TestMethod]
    public async Task AutenticarAsync_ComSenhaInvalida_RetornaErro()
    {
        // Arrange
        var email = "admin@test.com";
        var senha = "SenhaSegura123";
        var admin = new AdminUser
        {
            Id = 1,
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("OutraSenha"),
            Ativo = true
        };

        _mockRepository.Setup(r => r.ObterPorEmailAsync(email))
            .ReturnsAsync(admin);

        // Act
        var resultado = await _service.AutenticarAsync(email, senha);

        // Assert
        Assert.IsFalse(resultado.Sucesso);
    }

    [TestMethod]
    public async Task CriarAdminAsync_ComDadosValidos_RetornaSucesso()
    {
        // Arrange
        var email = "novo@test.com";
        var senha = "SenhaSegura123";

        _mockRepository.Setup(r => r.ExisteEmailAsync(email))
            .ReturnsAsync(false);

        // Act
        var resultado = await _service.CriarAdminAsync(email, senha);

        // Assert
        Assert.IsTrue(resultado.Sucesso);
        _mockRepository.Verify(r => r.AdicionarAsync(It.IsAny<AdminUser>()), Times.Once);
    }

    [TestMethod]
    public async Task CriarAdminAsync_ComEmailDuplicado_RetornaErro()
    {
        // Arrange
        var email = "existente@test.com";
        var senha = "SenhaSegura123";

        _mockRepository.Setup(r => r.ExisteEmailAsync(email))
            .ReturnsAsync(true);

        // Act
        var resultado = await _service.CriarAdminAsync(email, senha);

        // Assert
        Assert.IsFalse(resultado.Sucesso);
        _mockRepository.Verify(r => r.AdicionarAsync(It.IsAny<AdminUser>()), Times.Never);
    }
}
