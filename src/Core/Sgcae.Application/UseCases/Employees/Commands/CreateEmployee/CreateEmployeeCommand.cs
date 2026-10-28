using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Employees.Commands.CreateEmployee;

/// <summary>
/// Comando para registrar un empleado. Devuelve el Id del empleado creado.
/// </summary>
public sealed record CreateEmployeeCommand(
    string IdentificationNumber,
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    string Area) : IRequest<Guid>;
