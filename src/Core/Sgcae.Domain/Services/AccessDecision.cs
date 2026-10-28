using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Domain.Services;

/// <summary>
/// Resultado de evaluar una solicitud de acceso: autorizada, o rechazada con su motivo.
/// </summary>
public sealed record AccessDecision
{
    public AccessResult Result { get; }
    public RejectionReason? Reason { get; }

    public bool IsGranted => Result == AccessResult.Granted;

    private AccessDecision(AccessResult result, RejectionReason? reason)
    {
        Result = result;
        Reason = reason;
    }

    public static AccessDecision Grant() => new(AccessResult.Granted, null);

    public static AccessDecision Deny(RejectionReason reason) => new(AccessResult.Denied, reason);
}
