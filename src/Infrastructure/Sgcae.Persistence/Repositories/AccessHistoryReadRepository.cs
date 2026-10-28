using Microsoft.EntityFrameworkCore;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.AccessRecords;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Persistence.Extensions;

namespace Sgcae.Persistence.Repositories;

/// <summary>
/// Repositorio de solo lectura del historial (CQRS). Aplica los filtros sobre la tabla
/// de registros, la combina con empleados y puntos de acceso, y proyecta directamente al DTO,
/// de modo que SQL Server devuelve solo las columnas y la página que se necesitan.
/// </summary>
public sealed class AccessHistoryReadRepository(SgcaeDbContext context) : IAccessHistoryReadRepository
{
    public async Task<PaginationResponse<AccessHistoryItemDto>> GetPagedAsync(
        AccessHistoryFilter filter,
        PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        IQueryable<AccessRecord> records = context.Set<AccessRecord>().AsNoTracking();

        if (filter.EmployeeId.HasValue)
            records = records.Where(record => record.EmployeeId == filter.EmployeeId.Value);

        if (filter.AccessPointId.HasValue)
            records = records.Where(record => record.AccessPointId == filter.AccessPointId.Value);

        if (filter.Result.HasValue)
            records = records.Where(record => record.Result == filter.Result.Value);

        if (filter.FromUtc.HasValue)
            records = records.Where(record => record.OccurredAt >= filter.FromUtc.Value);

        if (filter.ToUtcExclusive.HasValue)
            records = records.Where(record => record.OccurredAt < filter.ToUtcExclusive.Value);

        IQueryable<AccessHistoryItemDto> history =
            from record in records
            join accessPoint in context.Set<AccessPoint>() on record.AccessPointId equals accessPoint.Id
            join employee in context.Set<Employee>() on record.EmployeeId equals (Guid?)employee.Id into employees
            from employee in employees.DefaultIfEmpty()
            orderby record.OccurredAt descending
            select new AccessHistoryItemDto
            {
                Id = record.Id,
                OccurredAt = record.OccurredAt,
                MovementType = record.MovementType,
                Result = record.Result,
                RejectionReason = record.RejectionReason,
                CredentialCode = record.CredentialCode,
                EmployeeId = record.EmployeeId,
                EmployeeIdentification = employee == null ? null : employee.Identification.Value,
                EmployeeFullName = employee == null ? null : employee.FirstName + " " + employee.LastName,
                AccessPointId = accessPoint.Id,
                AccessPointName = accessPoint.Name
            };

        return await history.ToPagedListAsync(pagination, cancellationToken);
    }
}
