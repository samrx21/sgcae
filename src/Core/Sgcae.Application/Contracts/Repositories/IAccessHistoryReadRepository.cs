using Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;
using Sgcae.Application.Utilities.Pagination;

namespace Sgcae.Application.Contracts.Repositories;

/// <summary>
/// Repositorio de solo lectura del historial de accesos (lado de las queries en CQRS).
/// Devuelve directamente los DTOs de la consulta, sin cargar entidades del dominio,
/// porque solo se leen y se combinan con datos de empleados y puntos de acceso.
/// </summary>
public interface IAccessHistoryReadRepository
{
    Task<PaginationResponse<AccessHistoryItemDto>> GetPagedAsync(
        AccessHistoryFilter filter,
        PaginationRequest pagination,
        CancellationToken cancellationToken = default);
}
