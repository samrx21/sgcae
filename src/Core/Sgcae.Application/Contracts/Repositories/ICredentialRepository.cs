using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.Contracts.Repositories;

/// <summary>
/// Operaciones propias del repositorio de credenciales.
/// </summary>
public interface ICredentialRepository : IRepository<Credential>
{
    Task<Credential?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Indica si el empleado ya tiene una credencial vigente (activa o bloqueada).
    /// </summary>
    Task<bool> HasCurrentCredentialAsync(Guid employeeId, CancellationToken cancellationToken = default);

    Task<PaginationResponse<Credential>> GetPagedListAsync(
        PaginationRequest pagination,
        Guid? employeeId,
        CredentialStatus? status,
        CancellationToken cancellationToken = default);
}
