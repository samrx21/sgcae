using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Queries.GetAccessPointList;

public sealed class GetAccessPointListUseCase(IAccessPointRepository accessPointRepository)
    : IRequestHandler<GetAccessPointListQuery, PaginationResponse<AccessPointDto>>
{
    public async Task<PaginationResponse<AccessPointDto>> Handle(
        GetAccessPointListQuery request, CancellationToken cancellationToken)
    {
        PaginationResponse<AccessPoint> page = await accessPointRepository.GetPagedListAsync(
            request.Pagination, request.Type, request.IsEnabled, cancellationToken);

        List<AccessPointDto> items = page.Items.Select(accessPoint => accessPoint.ToDto()).ToList();

        return PaginationResponse<AccessPointDto>.Create(items, page.TotalCount, request.Pagination);
    }
}
