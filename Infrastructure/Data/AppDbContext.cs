using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Core.Domain;

namespace GrupoArmaReforger.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Operador> Operadores { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Operador>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.SteamID)
                .HasMaxLength(50);

            entity.Property(e => e.PSN)
                .HasMaxLength(50);

            entity.Property(e => e.DataCriacao)
                .HasDefaultValue(DateTime.UtcNow);

            entity.HasIndex(e => e.Email)
                .IsUnique();
        });
    }
}
