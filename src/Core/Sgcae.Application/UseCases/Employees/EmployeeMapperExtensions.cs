using Sgcae.Application.UseCases.Employees.Queries.GetEmployeeById;
using Sgcae.Application.UseCases.Employees.Queries.GetEmployeeList;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees;

/// <summary>
/// Transformaciones de la entidad Employee hacia los DTOs que exponen los casos de uso.
/// </summary>
internal static class EmployeeMapperExtensions
{
    public static EmployeeDetailDto ToDetailDto(this Employee employee) =>
        new()
        {
            Id = employee.Id,
            IdentificationNumber = employee.Identification.Value,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            FullName = employee.FullName,
            Email = employee.Email.Value,
            JobTitle = employee.JobTitle,
            Area = employee.Area,
            Status = employee.Status,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        };

    public static EmployeeListItemDto ToListItemDto(this Employee employee) =>
        new()
        {
            Id = employee.Id,
            IdentificationNumber = employee.Identification.Value,
            FullName = employee.FullName,
            Email = employee.Email.Value,
            JobTitle = employee.JobTitle,
            Area = employee.Area,
            Status = employee.Status
        };
}
