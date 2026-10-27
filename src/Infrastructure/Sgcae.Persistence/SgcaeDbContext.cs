using Microsoft.EntityFrameworkCore;

namespace Sgcae.Persistence;

/// <summary>
/// Contexto de datos del sistema. No declara un DbSet por entidad: cada entidad se registra
/// con su propia clase de configuración (carpeta Configurations) y los repositorios acceden
/// a ella con Set&lt;T&gt;(). Así, agregar una entidad nueva no obliga a modificar este archivo.
/// </summary>
public sealed class SgcaeDbContext(DbContextOptions<SgcaeDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgcaeDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
