namespace Sgcae.Domain.Entities.Credentials;

/// <summary>
/// Estado de una credencial. Solo una credencial activa permite el acceso.
/// Una credencial bloqueada se puede desbloquear; una revocada queda anulada para siempre.
/// </summary>
public enum CredentialStatus
{
    Active = 1,
    Blocked = 2,
    Revoked = 3
}
