using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Application.Contracts.Repositories;

/// <summary>
/// Repositorio de escritura de los registros de acceso (lado de los commands en CQRS).
/// </summary>
public interface IAccessRecordRepository : IRepository<AccessRecord>
{
    /// <summary>
    /// Último movimiento autorizado del empleado, o null si nunca ha ingresado.
    /// Sirve para saber si el empleado está adentro o afuera.
    /// </summary>
    Task<MovementType?> GetLastGrantedMovementAsync(Guid employeeId, CancellationToken cancellationToken = default);
}
