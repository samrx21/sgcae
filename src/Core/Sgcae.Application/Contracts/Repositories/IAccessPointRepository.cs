using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.Contracts.Repositories;

/// <summary>
/// Operaciones propias del repositorio de puntos de acceso.
/// </summary>
public interface IAccessPointRepository : IRepository<AccessPoint>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<PaginationResponse<AccessPoint>> GetPagedListAsync(
        PaginationRequest pagination,
        AccessPointType? type,
        bool? isEnabled,
        CancellationToken cancellationToken = default);
}
