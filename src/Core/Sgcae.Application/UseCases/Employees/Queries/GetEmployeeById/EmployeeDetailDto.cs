using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Queries.GetEmployeeById;

/// <summary>
/// Detalle completo de un empleado. Nunca se expone la entidad del dominio directamente.
/// </summary>
public sealed class EmployeeDetailDto
{
    public Guid Id { get; init; }
    public string IdentificationNumber { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string JobTitle { get; init; } = null!;
    public string Area { get; init; } = null!;
    public EmploymentStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
