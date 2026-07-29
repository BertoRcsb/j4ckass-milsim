using Microsoft.EntityFrameworkCore;
using GrupoArmaReforger.Pages.Models;

namespace GrupoArmaReforger.Data;

public class AppDbContext : DbContext
{
    public DbSet<Operador> Operadores { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
