using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
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

// Configurar ForwardedHeaders para proxy (Caddy)
builder.Services.AddHttpLogging(options => { });
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// Adicionar autenticação com cookies
builder.Services.AddAuthentication("AdminScheme")
    .AddCookie("AdminScheme", options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Admin/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

// Adicionar autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAuthenticatedUser());
});

// Adicionar serviços de apresentação
builder.Services.AddRazorPages();

// Configurar banco de dados (SQLite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=app.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Registrar serviços da aplicação (Dependency Injection)
RegisterApplicationServices(builder.Services);

// Adicionar logging
builder.Services.AddLogging();

var app = builder.Build();

// Inicializar banco de dados com dados padrão
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InitializeAsync(dbContext);
}

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
    services.AddScoped<IAdminRepository, AdminRepository>();
    services.AddScoped<IAvisoRepository, AvisoRepository>();
    services.AddScoped<IAtualizacaoRepository, AtualizacaoRepository>();

    // Application Services - Lógica de negócio
    services.AddScoped<IRecrutamentoService, RecrutamentoService>();
    services.AddScoped<IAdminService, AdminService>();
    services.AddScoped<AvisoService>();
    services.AddScoped<AtualizacaoService>();

    // Infrastructure Services - Assets e recursos
    services.AddSingleton<IAssetService, AssetService>();
}

/// <summary>
/// Configura o pipeline de requisições HTTP
/// </summary>
static void ConfigureHttpPipeline(WebApplication app)
{
    // ForwardedHeaders deve ser o primeiro middleware
    app.UseForwardedHeaders();

    // Tratamento de erros
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    // Middleware de segurança e roteamento
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    // Mapear assets estáticos e páginas Razor
    app.MapStaticAssets();
    app.MapRazorPages()
       .WithStaticAssets();
}
