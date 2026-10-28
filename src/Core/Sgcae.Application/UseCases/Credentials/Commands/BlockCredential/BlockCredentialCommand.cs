using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Credentials.Commands.BlockCredential;

/// <summary>
/// Comando para bloquear temporalmente una credencial, indicando el motivo.
/// </summary>
public sealed record BlockCredentialCommand(string Code, string Reason) : IRequest;
