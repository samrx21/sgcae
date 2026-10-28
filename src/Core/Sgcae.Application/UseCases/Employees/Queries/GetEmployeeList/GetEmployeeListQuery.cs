using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Queries.GetEmployeeList;

/// <summary>
/// Query para listar empleados de forma paginada. Permite buscar por nombre,
/// identificación o correo, y filtrar por estado y área.
/// </summary>
public sealed record GetEmployeeListQuery : IRequest<PaginationResponse<EmployeeListItemDto>>
{
    public PaginationRequest Pagination { get; init; } = PaginationRequest.Standard();
    public string? Search { get; init; }
    public EmploymentStatus? Status { get; init; }
    public string? Area { get; init; }
}
