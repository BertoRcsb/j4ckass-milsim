using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;
using GrupoArmaReforger.Core.Constants;

namespace GrupoArmaReforger.Infrastructure.Data;

/// <summary>
/// DbContext da aplicação
/// Configura mapeamento objeto-relacional e convenções do banco de dados
/// Utiliza SQLite como provedor de dados
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// DbSet para operações CRUD de Operador
    /// </summary>
    public DbSet<Operador> Operadores { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configura mapeamento das entidades para o banco de dados
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarOperador(modelBuilder);
    }

    /// <summary>
    /// Configura a entidade Operador com validações e restrições do banco
    /// </summary>
    private static void ConfigurarOperador(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Operador>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxNomeLength);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxEmailLength);

            entity.Property(e => e.SteamID)
                .HasMaxLength(AppConstants.Validation.MaxSteamIdLength);

            entity.Property(e => e.PSN)
                .HasMaxLength(AppConstants.Validation.MaxPsnLength);

            entity.Property(e => e.DataCriacao)
                .HasDefaultValue(DateTime.UtcNow);

            // Índice único para evitar emails duplicados
            entity.HasIndex(e => e.Email)
                .IsUnique();
        });
    }
}
