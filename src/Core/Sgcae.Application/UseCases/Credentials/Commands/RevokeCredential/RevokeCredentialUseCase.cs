using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.UseCases.Credentials.Commands.RevokeCredential;

public sealed class RevokeCredentialUseCase(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<RevokeCredentialCommand>
{
    public async Task Handle(RevokeCredentialCommand request, CancellationToken cancellationToken)
    {
        Credential credential = await credentialRepository.GetByCodeAsync(request.Code, cancellationToken)
            ?? throw new NotFoundException($"No existe una credencial con el código {request.Code}.");

        credential.Revoke(request.Reason);

        await credentialRepository.UpdateAsync(credential, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
