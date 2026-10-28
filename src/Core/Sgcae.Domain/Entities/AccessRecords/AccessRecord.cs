using Sgcae.Domain.Exceptions;
using Sgcae.Domain.Services;

namespace Sgcae.Domain.Entities.AccessRecords;

/// <summary>
/// Registro de un intento de entrada o salida, autorizado o rechazado. Es la traza de auditoría
/// del sistema: una vez creado no se modifica ni se elimina, por eso no tiene métodos de cambio.
/// </summary>
public sealed class AccessRecord
{
    /// <summary>Largo máximo del código presentado. Se guarda aunque no tenga el formato de una credencial.</summary>
    public const int PRESENTED_CODE_MAX_LENGTH = 50;

    public Guid Id { get; private set; }

    /// <summary>Código tal como se presentó en el punto de acceso, aunque no exista.</summary>
    public string CredentialCode { get; private set; } = null!;

    public Guid? CredentialId { get; private set; }
    public Guid? EmployeeId { get; private set; }
    public Guid AccessPointId { get; private set; }
    public MovementType MovementType { get; private set; }
    public AccessResult Result { get; private set; }
    public RejectionReason? RejectionReason { get; private set; }
    public DateTime OccurredAt { get; private set; }

    // Requerido por EF Core para materializar la entidad
    private AccessRecord() { }

    private AccessRecord(
        string credentialCode,
        Guid? credentialId,
        Guid? employeeId,
        Guid accessPointId,
        MovementType movementType,
        AccessDecision decision)
    {
        Id = Guid.CreateVersion7();
        CredentialCode = credentialCode;
        CredentialId = credentialId;
        EmployeeId = employeeId;
        AccessPointId = accessPointId;
        MovementType = movementType;
        Result = decision.Result;
        RejectionReason = decision.Reason;
        OccurredAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Registra el resultado de una solicitud de acceso, sea autorizada o rechazada.
    /// </summary>
    public static AccessRecord Register(
        string credentialCode,
        Guid? credentialId,
        Guid? employeeId,
        Guid accessPointId,
        MovementType movementType,
        AccessDecision decision)
    {
        string normalizedCode = Credentials.ValueObjects.CredentialCode.Normalize(credentialCode);

        if (string.IsNullOrWhiteSpace(normalizedCode))
            throw new BusinessRuleException("El código de la credencial es requerido.");

        if (normalizedCode.Length > PRESENTED_CODE_MAX_LENGTH)
            throw new BusinessRuleException(
                $"El código de la credencial no puede superar {PRESENTED_CODE_MAX_LENGTH} caracteres.");

        if (accessPointId == Guid.Empty)
            throw new BusinessRuleException("El punto de acceso es requerido.");

        if (!Enum.IsDefined(movementType))
            throw new BusinessRuleException("El tipo de movimiento no es válido.");

        if (decision is null)
            throw new BusinessRuleException("La decisión de acceso es requerida.");

        // Un acceso autorizado siempre debe saber quién entró y con qué credencial
        if (decision.IsGranted && (credentialId is null || employeeId is null))
            throw new BusinessRuleException("Un acceso autorizado debe tener credencial y empleado.");

        return new AccessRecord(normalizedCode, credentialId, employeeId, accessPointId, movementType, decision);
    }
}
