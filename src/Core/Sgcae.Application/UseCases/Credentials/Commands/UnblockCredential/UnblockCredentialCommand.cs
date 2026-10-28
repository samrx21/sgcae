using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Credentials.Commands.UnblockCredential;

/// <summary>
/// Comando para volver a activar una credencial bloqueada.
/// </summary>
public sealed record UnblockCredentialCommand(string Code) : IRequest;
