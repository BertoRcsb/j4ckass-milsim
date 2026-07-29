using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Infrastructure.Data;
using GrupoArmaReforger.Infrastructure.Repositories;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<IOperadorRepository, OperadorRepository>();
builder.Services.AddScoped<IRecrutamentoService, RecrutamentoService>();
builder.Services.AddSingleton<IAssetService, AssetService>();

builder.Services.AddLogging();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
