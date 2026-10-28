using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Queries.GetEmployeeList;

/// <summary>
/// Datos resumidos de un empleado para mostrar en un listado.
/// </summary>
public sealed class EmployeeListItemDto
{
    public Guid Id { get; init; }
    public string IdentificationNumber { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string JobTitle { get; init; } = null!;
    public string Area { get; init; } = null!;
    public EmploymentStatus Status { get; init; }
}
