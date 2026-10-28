using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Queries.GetAccessPointById;

public sealed class GetAccessPointByIdUseCase(IAccessPointRepository accessPointRepository)
    : IRequestHandler<GetAccessPointByIdQuery, AccessPointDto>
{
    public async Task<AccessPointDto> Handle(GetAccessPointByIdQuery request, CancellationToken cancellationToken)
    {
        AccessPoint accessPoint = await accessPointRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"No existe un punto de acceso con el Id {request.Id}.");

        return accessPoint.ToDto();
    }
}
