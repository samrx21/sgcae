namespace Sgcae.Domain.Entities.AccessRecords;

/// <summary>
/// Motivo por el que se rechazó una solicitud de acceso.
/// </summary>
public enum RejectionReason
{
    CredentialNotFound = 1,
    CredentialBlocked = 2,
    CredentialRevoked = 3,
    EmployeeInactive = 4,
    AccessPointDisabled = 5,
    AlreadyInside = 6,
    NotInside = 7
}

public static class RejectionReasonExtensions
{
    /// <summary>
    /// Explicación del motivo en el lenguaje del negocio, para mostrarla al personal de seguridad.
    /// </summary>
    public static string ToDescription(this RejectionReason reason) => reason switch
    {
        RejectionReason.CredentialNotFound => "La credencial no está registrada.",
        RejectionReason.CredentialBlocked => "La credencial está bloqueada.",
        RejectionReason.CredentialRevoked => "La credencial fue revocada.",
        RejectionReason.EmployeeInactive => "El empleado no tiene un vínculo laboral activo.",
        RejectionReason.AccessPointDisabled => "El punto de acceso está inhabilitado.",
        RejectionReason.AlreadyInside => "El empleado ya registró una entrada y no ha registrado su salida.",
        RejectionReason.NotInside => "El empleado no tiene una entrada registrada para poder salir.",
        _ => "Acceso rechazado."
    };
}
