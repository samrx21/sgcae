using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Employees.Commands.UpdateEmployee;

/// <summary>
/// Comando para actualizar los datos de un empleado. La identificación no se puede cambiar.
/// </summary>
public sealed record UpdateEmployeeCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    string Area) : IRequest;
