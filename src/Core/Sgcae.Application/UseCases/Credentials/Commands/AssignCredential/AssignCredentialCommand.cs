using Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;
using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Credentials.Commands.AssignCredential;

/// <summary>
/// Comando para asignar una credencial nueva a un empleado. El código lo genera el sistema
/// y se devuelve en la respuesta.
/// </summary>
public sealed record AssignCredentialCommand(Guid EmployeeId) : IRequest<CredentialDetailDto>;
