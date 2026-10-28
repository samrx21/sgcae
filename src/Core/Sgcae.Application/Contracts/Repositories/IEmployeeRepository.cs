using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.Contracts.Repositories;

/// <summary>
/// Operaciones propias del repositorio de empleados.
/// </summary>
public interface IEmployeeRepository : IRepository<Employee>
{
    Task<bool> ExistsByIdentificationAsync(string identification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Indica si el correo ya pertenece a otro empleado. <paramref name="excludedEmployeeId"/>
    /// permite ignorar al propio empleado cuando se actualizan sus datos.
    /// </summary>
    Task<bool> ExistsByEmailAsync(
        string email, Guid? excludedEmployeeId = null, CancellationToken cancellationToken = default);

    Task<PaginationResponse<Employee>> GetPagedListAsync(
        PaginationRequest pagination,
        string? search,
        EmploymentStatus? status,
        string? area,
        CancellationToken cancellationToken = default);
}
