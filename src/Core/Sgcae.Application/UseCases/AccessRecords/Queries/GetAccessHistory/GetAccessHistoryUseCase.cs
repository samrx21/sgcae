using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;

namespace Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;

public sealed class GetAccessHistoryUseCase(IAccessHistoryReadRepository accessHistoryRepository)
    : IRequestHandler<GetAccessHistoryQuery, PaginationResponse<AccessHistoryItemDto>>
{
    public async Task<PaginationResponse<AccessHistoryItemDto>> Handle(
        GetAccessHistoryQuery request, CancellationToken cancellationToken)
    {
        if (request.From is DateOnly from && request.To is DateOnly to && from > to)
            throw new ValidationException("La fecha inicial no puede ser posterior a la fecha final.");

        // Las fechas se toman como días completos: "hasta" incluye todo ese día
        AccessHistoryFilter filter = new(
            request.EmployeeId,
            request.From?.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
            request.To?.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
            request.AccessPointId,
            request.Result);

        return await accessHistoryRepository.GetPagedAsync(filter, request.Pagination, cancellationToken);
    }
}
