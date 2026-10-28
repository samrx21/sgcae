using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.UseCases.Credentials.Queries.GetCredentialList;

/// <summary>
/// Datos resumidos de una credencial para mostrar en un listado.
/// </summary>
public sealed class CredentialListItemDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public Guid EmployeeId { get; init; }
    public CredentialStatus Status { get; init; }
    public bool CanBeUsed { get; init; }
    public DateTime IssuedAt { get; init; }
}
