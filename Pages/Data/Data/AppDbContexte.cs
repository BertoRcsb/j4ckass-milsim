using Microsoft.EntityFrameworkCore;
using MyApp.Namespace;

public class AppDbContext : DbContext
{
    public DbSet<Operador> Operadores { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
}