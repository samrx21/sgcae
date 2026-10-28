using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;

/// <summary>
/// Detalle de una credencial con los datos básicos de su empleado.
/// </summary>
public sealed class CredentialDetailDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public Guid EmployeeId { get; init; }
    public string EmployeeFullName { get; init; } = null!;
    public CredentialStatus Status { get; init; }
    public bool CanBeUsed { get; init; }
    public DateTime IssuedAt { get; init; }
    public DateTime? StatusChangedAt { get; init; }
    public string? StatusReason { get; init; }
}
