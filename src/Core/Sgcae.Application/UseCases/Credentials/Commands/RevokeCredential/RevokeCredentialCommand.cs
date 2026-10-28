using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Credentials.Commands.RevokeCredential;

/// <summary>
/// Comando para revocar una credencial de forma definitiva, indicando el motivo.
/// </summary>
public sealed record RevokeCredentialCommand(string Code, string Reason) : IRequest;
