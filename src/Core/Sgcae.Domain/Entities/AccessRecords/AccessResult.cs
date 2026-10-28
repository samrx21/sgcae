namespace Sgcae.Domain.Entities.AccessRecords;

/// <summary>
/// Resultado de la validación de una solicitud de acceso.
/// </summary>
public enum AccessResult
{
    Granted = 1,   // Autorizado
    Denied = 2     // Rechazado
}
