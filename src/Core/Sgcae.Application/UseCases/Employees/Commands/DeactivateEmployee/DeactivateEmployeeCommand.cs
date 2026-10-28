using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Employees.Commands.DeactivateEmployee;

/// <summary>
/// Comando para desactivar el vínculo laboral de un empleado. Los empleados no se eliminan,
/// para conservar la trazabilidad de sus accesos.
/// </summary>
public sealed record DeactivateEmployeeCommand(Guid Id) : IRequest;
