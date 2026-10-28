using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Employees.Queries.GetEmployeeById;

/// <summary>
/// Query para consultar el detalle de un empleado por su Id.
/// </summary>
public sealed record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDetailDto>;
