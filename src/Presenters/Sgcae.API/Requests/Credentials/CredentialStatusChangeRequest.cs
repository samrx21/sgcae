namespace Sgcae.API.Requests.Credentials;

/// <summary>
/// Cuerpo de la petición para bloquear o revocar una credencial: el motivo del cambio.
/// </summary>
public sealed record CredentialStatusChangeRequest(string Reason);
