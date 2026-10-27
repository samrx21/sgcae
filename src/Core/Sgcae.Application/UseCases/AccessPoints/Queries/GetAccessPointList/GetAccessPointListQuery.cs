using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Queries.GetAccessPointList;

/// <summary>
/// Query para listar los puntos de acceso de forma paginada, filtrando por tipo
/// y por si están habilitados.
/// </summary>
public sealed record GetAccessPointListQuery : IRequest<PaginationResponse<AccessPointDto>>
{
    public PaginationRequest Pagination { get; init; } = PaginationRequest.Standard();
    public AccessPointType? Type { get; init; }
    public bool? IsEnabled { get; init; }
}
