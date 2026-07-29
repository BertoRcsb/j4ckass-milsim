using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Infrastructure.Data;
using GrupoArmaReforger.Infrastructure.Repositories;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.Services;

/// <summary>
/// Aplicação ASP.NET Core - J4CKASS MILSIM
/// Portal web para comunidade de Arma Reforger
///
/// Implementa Clean Architecture com 4 camadas:
/// - Core: Domínio e interfaces
/// - Application: Serviços e DTOs
/// - Infrastructure: Persistência e recursos
/// - Pages: Apresentação (Razor Pages)
/// </summary>

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços de apresentação
builder.Services.AddRazorPages();

// Configurar banco de dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Registrar serviços da aplicação (Dependency Injection)
RegisterApplicationServices(builder.Services);

// Adicionar logging
builder.Services.AddLogging();

var app = builder.Build();

// Configurar pipeline HTTP
ConfigureHttpPipeline(app);

app.Run();

/// <summary>
/// Registra todos os serviços da aplicação no DI Container
/// </summary>
static void RegisterApplicationServices(IServiceCollection services)
{
    // Repositories - Padrão Repository
    services.AddScoped<IOperadorRepository, OperadorRepository>();

    // Application Services - Lógica de negócio
    services.AddScoped<IRecrutamentoService, RecrutamentoService>();

    // Infrastructure Services - Assets e recursos
    services.AddSingleton<IAssetService, AssetService>();
}

/// <summary>
/// Configura o pipeline de requisições HTTP
/// </summary>
static void ConfigureHttpPipeline(WebApplication app)
{
    // Tratamento de erros
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    // Middleware de segurança e roteamento
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();

    // Mapear assets estáticos e páginas Razor
    app.MapStaticAssets();
    app.MapRazorPages()
       .WithStaticAssets();
}
