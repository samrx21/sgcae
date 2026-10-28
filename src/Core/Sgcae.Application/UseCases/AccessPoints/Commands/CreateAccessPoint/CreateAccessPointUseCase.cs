using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Commands.CreateAccessPoint;

public sealed class CreateAccessPointUseCase(IAccessPointRepository accessPointRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAccessPointCommand, Guid>
{
    public async Task<Guid> Handle(CreateAccessPointCommand request, CancellationToken cancellationToken)
    {
        // El dominio valida el nombre, el tipo y la ubicación
        AccessPoint accessPoint = new(request.Name, request.Type, request.Location);

        if (await accessPointRepository.ExistsByNameAsync(accessPoint.Name, cancellationToken))
            throw new ConflictException($"Ya existe un punto de acceso con el nombre {accessPoint.Name}.");

        await accessPointRepository.AddAsync(accessPoint, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return accessPoint.Id;
    }
}
