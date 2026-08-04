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

    /// <summary>
    /// DbSet para operações CRUD de Aviso
    /// </summary>
    public DbSet<Aviso> Avisos { get; set; }

    /// <summary>
    /// DbSet para operações CRUD de Atualizacao
    /// </summary>
    public DbSet<Atualizacao> Atualizacoes { get; set; }

    /// <summary>
    /// DbSet para operações CRUD de AdminUser
    /// </summary>
    public DbSet<AdminUser> AdminUsers { get; set; }

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
        ConfigurarAdminUser(modelBuilder);
        ConfigurarAviso(modelBuilder);
        ConfigurarAtualizacao(modelBuilder);
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
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Índice único para evitar emails duplicados
            entity.HasIndex(e => e.Email)
                .IsUnique();
        });
    }

    /// <summary>
    /// Configura a entidade AdminUser com validações e restrições do banco
    /// </summary>
    private static void ConfigurarAdminUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxEmailLength);

            entity.Property(e => e.SenhaHash)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxSenhaLength);

            entity.Property(e => e.Ativo)
                .HasDefaultValue(true);

            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Índice único para evitar emails duplicados
            entity.HasIndex(e => e.Email)
                .IsUnique();

            // Relacionamento 1:N com Avisos
            entity.HasMany(e => e.Avisos)
                .WithOne(a => a.AdminUser)
                .HasForeignKey(a => a.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento 1:N com Atualizacoes
            entity.HasMany(e => e.Atualizacoes)
                .WithOne(a => a.AdminUser)
                .HasForeignKey(a => a.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    /// <summary>
    /// Configura a entidade Aviso com validações e restrições do banco
    /// </summary>
    private static void ConfigurarAviso(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aviso>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxTituloLength);

            entity.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxConteudoLength);

            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Índice para ordenação por data
            entity.HasIndex(e => e.DataCriacao)
                .IsDescending();
        });
    }

    /// <summary>
    /// Configura a entidade Atualizacao com validações e restrições do banco
    /// </summary>
    private static void ConfigurarAtualizacao(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atualizacao>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Versao)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxVersaoLength);

            entity.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(AppConstants.Validation.MaxConteudoLength);

            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Índice para ordenação por data
            entity.HasIndex(e => e.DataCriacao)
                .IsDescending();
        });
    }
}
