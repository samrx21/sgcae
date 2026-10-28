using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.UseCases.Credentials.Commands.BlockCredential;

public sealed class BlockCredentialUseCase(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<BlockCredentialCommand>
{
    public async Task Handle(BlockCredentialCommand request, CancellationToken cancellationToken)
    {
        Credential credential = await credentialRepository.GetByCodeAsync(request.Code, cancellationToken)
            ?? throw new NotFoundException($"No existe una credencial con el código {request.Code}.");

        credential.Block(request.Reason);

        await credentialRepository.UpdateAsync(credential, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
