using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Persistence.UnitOfWorks;

namespace Sgcae.Persistence;

public static class PersistenceServicesRegistry
{
    public const string CONNECTION_STRING_NAME = "DefaultConnection";

    /// <summary>
    /// Registra el DbContext, el Unit of Work y todos los repositorios. La cadena de conexión
    /// la define el presentador (API): persistencia sabe el "cómo", no el "dónde".
    /// </summary>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME)
            ?? throw new InvalidOperationException(
                $"La cadena de conexión '{CONNECTION_STRING_NAME}' no está configurada.");

        services.AddDbContext<SgcaeDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRepositories(typeof(PersistenceServicesRegistry).Assembly);

        return services;
    }

    /// <summary>
    /// Aplica las migraciones pendientes al iniciar la API, para que la base de datos
    /// se cree sola al ejecutar el proyecto por primera vez.
    /// </summary>
    public static async Task ApplyMigrationsAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = services.CreateScope();
        SgcaeDbContext context = scope.ServiceProvider.GetRequiredService<SgcaeDbContext>();
        await context.Database.MigrateAsync(cancellationToken);
    }

    /// <summary>
    /// Registra cada repositorio con la interfaz que implementa (definida en
    /// Application/Contracts/Repositories). Así, agregar un repositorio nuevo no obliga
    /// a modificar este archivo.
    /// </summary>
    private static void AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        string contractsNamespace = typeof(IRepository<>).Namespace!;

        IEnumerable<Type> repositoryTypes = assembly.GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false });

        foreach (Type repositoryType in repositoryTypes)
        {
            IEnumerable<Type> contracts = repositoryType.GetInterfaces()
                .Where(@interface => @interface.Namespace == contractsNamespace && !@interface.IsGenericType);

            foreach (Type contract in contracts)
                services.AddScoped(contract, repositoryType);
        }
    }
}
