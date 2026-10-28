using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;

public sealed class GetCredentialByCodeUseCase(
    ICredentialRepository credentialRepository,
    IEmployeeRepository employeeRepository) : IRequestHandler<GetCredentialByCodeQuery, CredentialDetailDto>
{
    public async Task<CredentialDetailDto> Handle(GetCredentialByCodeQuery request, CancellationToken cancellationToken)
    {
        Credential credential = await credentialRepository.GetByCodeAsync(request.Code, cancellationToken)
            ?? throw new NotFoundException($"No existe una credencial con el código {request.Code}.");

        Employee employee = await employeeRepository.GetByIdAsync(credential.EmployeeId, cancellationToken)
            ?? throw new NotFoundException($"No existe el empleado {credential.EmployeeId} de la credencial.");

        return credential.ToDetailDto(employee);
    }
}
