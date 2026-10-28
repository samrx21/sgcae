using Microsoft.EntityFrameworkCore;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Persistence.Extensions;

namespace Sgcae.Persistence.Repositories;

public sealed class EmployeeRepository(SgcaeDbContext context) : Repository<Employee>(context), IEmployeeRepository
{
    public async Task<bool> ExistsByIdentificationAsync(string identification, CancellationToken cancellationToken = default)
    {
        string value = identification.Trim();
        return await Context.Set<Employee>().AnyAsync(employee => employee.Identification.Value == value, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email, Guid? excludedEmployeeId = null, CancellationToken cancellationToken = default)
    {
        string value = email.Trim().ToLowerInvariant();
        return await Context.Set<Employee>().AnyAsync(
            employee => employee.Email.Value == value && employee.Id != excludedEmployeeId,
            cancellationToken);
    }

    public async Task<PaginationResponse<Employee>> GetPagedListAsync(
        PaginationRequest pagination,
        string? search,
        EmploymentStatus? status,
        string? area,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Employee> query = Context.Set<Employee>().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            string term = search.Trim();
            query = query.Where(employee =>
                employee.FirstName.Contains(term)
                || employee.LastName.Contains(term)
                || employee.Identification.Value.Contains(term)
                || employee.Email.Value.Contains(term));
        }

        if (status.HasValue)
            query = query.Where(employee => employee.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(area))
        {
            string areaName = area.Trim();
            query = query.Where(employee => employee.Area == areaName);
        }

        return await query
            .OrderBy(employee => employee.LastName)
            .ThenBy(employee => employee.FirstName)
            .ThenBy(employee => employee.Id)
            .ToPagedListAsync(pagination, cancellationToken);
    }
}
