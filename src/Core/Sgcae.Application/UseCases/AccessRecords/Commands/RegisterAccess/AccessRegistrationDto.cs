using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Application.UseCases.AccessRecords.Commands.RegisterAccess;

/// <summary>
/// Resultado de una solicitud de acceso: si se autorizó o se rechazó, y por qué.
/// </summary>
public sealed class AccessRegistrationDto
{
    public Guid AccessRecordId { get; init; }
    public AccessResult Result { get; init; }
    public RejectionReason? RejectionReason { get; init; }
    public string Message { get; init; } = null!;
    public MovementType MovementType { get; init; }
    public string CredentialCode { get; init; } = null!;
    public Guid? EmployeeId { get; init; }
    public string? EmployeeFullName { get; init; }
    public Guid AccessPointId { get; init; }
    public string AccessPointName { get; init; } = null!;
    public DateTime OccurredAt { get; init; }
}
