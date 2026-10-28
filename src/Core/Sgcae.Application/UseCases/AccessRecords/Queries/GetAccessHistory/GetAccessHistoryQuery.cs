using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;

/// <summary>
/// Query para consultar el historial de accesos de forma paginada. Todos los filtros son
/// opcionales y se pueden combinar: empleado, rango de fechas, punto de acceso y resultado.
/// </summary>
public sealed record GetAccessHistoryQuery : IRequest<PaginationResponse<AccessHistoryItemDto>>
{
    public PaginationRequest Pagination { get; init; } = PaginationRequest.Standard();
    public Guid? EmployeeId { get; init; }
    public DateOnly? From { get; init; }
    public DateOnly? To { get; init; }
    public Guid? AccessPointId { get; init; }
    public AccessResult? Result { get; init; }
}
