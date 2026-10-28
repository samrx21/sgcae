using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.UseCases.Credentials.Queries.GetCredentialList;

/// <summary>
/// Query para listar credenciales de forma paginada, filtrando por empleado y por estado.
/// </summary>
public sealed record GetCredentialListQuery : IRequest<PaginationResponse<CredentialListItemDto>>
{
    public PaginationRequest Pagination { get; init; } = PaginationRequest.Standard();
    public Guid? EmployeeId { get; init; }
    public CredentialStatus? Status { get; init; }
}
