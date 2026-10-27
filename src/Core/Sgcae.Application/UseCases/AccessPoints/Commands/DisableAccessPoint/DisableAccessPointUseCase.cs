using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Commands.DisableAccessPoint;

public sealed class DisableAccessPointUseCase(IAccessPointRepository accessPointRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DisableAccessPointCommand>
{
    public async Task Handle(DisableAccessPointCommand request, CancellationToken cancellationToken)
    {
        AccessPoint accessPoint = await accessPointRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"No existe un punto de acceso con el Id {request.Id}.");

        accessPoint.Disable();

        await accessPointRepository.UpdateAsync(accessPoint, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
