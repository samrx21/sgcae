using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Credentials.ValueObjects;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Credentials.Commands.AssignCredential;

public sealed class AssignCredentialUseCase(
    ICredentialRepository credentialRepository,
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AssignCredentialCommand, CredentialDetailDto>
{
    private const int MAX_CODE_ATTEMPTS = 5;

    public async Task<CredentialDetailDto> Handle(AssignCredentialCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken)
            ?? throw new NotFoundException($"No existe un empleado con el Id {request.EmployeeId}.");

        // Un empleado solo puede tener una credencial vigente a la vez
        if (await credentialRepository.HasCurrentCredentialAsync(employee.Id, cancellationToken))
            throw new ConflictException(
                "El empleado ya tiene una credencial activa o bloqueada. Revóquela antes de asignar una nueva.");

        CredentialCode code = await GenerateUniqueCodeAsync(cancellationToken);

        // El dominio valida que el empleado esté activo
        Credential credential = Credential.Issue(employee, code);

        await credentialRepository.AddAsync(credential, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return credential.ToDetailDto(employee);
    }

    /// <summary>
    /// Genera un código y comprueba que no exista. Una repetición es muy improbable,
    /// pero si ocurre se intenta de nuevo.
    /// </summary>
    private async Task<CredentialCode> GenerateUniqueCodeAsync(CancellationToken cancellationToken)
    {
        for (int attempt = 0; attempt < MAX_CODE_ATTEMPTS; attempt++)
        {
            CredentialCode code = CredentialCode.Generate();
            if (!await credentialRepository.ExistsByCodeAsync(code.Value, cancellationToken))
                return code;
        }

        throw new ConflictException("No fue posible generar un código de credencial único. Intente de nuevo.");
    }
}
