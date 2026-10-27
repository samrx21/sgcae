using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Commands.EnableAccessPoint;

public sealed class EnableAccessPointUseCase(IAccessPointRepository accessPointRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<EnableAccessPointCommand>
{
    public async Task Handle(EnableAccessPointCommand request, CancellationToken cancellationToken)
    {
        AccessPoint accessPoint = await accessPointRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"No existe un punto de acceso con el Id {request.Id}.");

        accessPoint.Enable();

        await accessPointRepository.UpdateAsync(accessPoint, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
