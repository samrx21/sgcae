using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;

/// <summary>
/// Un movimiento del historial, con los datos del empleado y del punto de acceso ya combinados.
/// </summary>
public sealed class AccessHistoryItemDto
{
    public Guid Id { get; init; }
    public DateTime OccurredAt { get; init; }
    public MovementType MovementType { get; init; }
    public AccessResult Result { get; init; }
    public RejectionReason? RejectionReason { get; init; }
    public string CredentialCode { get; init; } = null!;
    public Guid? EmployeeId { get; init; }
    public string? EmployeeIdentification { get; init; }
    public string? EmployeeFullName { get; init; }
    public Guid AccessPointId { get; init; }
    public string AccessPointName { get; init; } = null!;

    /// <summary>
    /// Explicación del rechazo en palabras. Se calcula al responder, no se guarda.
    /// </summary>
    public string? RejectionDescription => RejectionReason?.ToDescription();
}
