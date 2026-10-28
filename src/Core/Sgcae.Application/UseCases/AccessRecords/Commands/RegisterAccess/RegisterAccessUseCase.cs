using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.AccessRecords;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Services;

namespace Sgcae.Application.UseCases.AccessRecords.Commands.RegisterAccess;

/// <summary>
/// Valida una solicitud de acceso y registra su resultado. Los accesos rechazados también
/// se guardan, para que el historial muestre todos los intentos.
/// </summary>
public sealed class RegisterAccessUseCase(
    IAccessPointRepository accessPointRepository,
    ICredentialRepository credentialRepository,
    IEmployeeRepository employeeRepository,
    IAccessRecordRepository accessRecordRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterAccessCommand, AccessRegistrationDto>
{
    public async Task<AccessRegistrationDto> Handle(RegisterAccessCommand request, CancellationToken cancellationToken)
    {
        // Sin un punto de acceso válido no hay dónde registrar el intento
        AccessPoint accessPoint = await accessPointRepository.GetByIdAsync(request.AccessPointId, cancellationToken)
            ?? throw new NotFoundException($"No existe un punto de acceso con el Id {request.AccessPointId}.");

        // 1. Reunir los datos que necesita la regla de negocio
        Credential? credential = await credentialRepository.GetByCodeAsync(request.CredentialCode, cancellationToken);

        Employee? employee = credential is null
            ? null
            : await employeeRepository.GetByIdAsync(credential.EmployeeId, cancellationToken);

        MovementType? lastGrantedMovement = employee is null
            ? null
            : await accessRecordRepository.GetLastGrantedMovementAsync(employee.Id, cancellationToken);

        // 2. El dominio decide si se autoriza o se rechaza
        AccessDecision decision = AccessPolicy.Evaluate(
            credential, employee, accessPoint, request.MovementType, lastGrantedMovement);

        // 3. Se registra el resultado, sea cual sea
        AccessRecord record = AccessRecord.Register(
            request.CredentialCode, credential?.Id, employee?.Id, accessPoint.Id, request.MovementType, decision);

        await accessRecordRepository.AddAsync(record, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new AccessRegistrationDto
        {
            AccessRecordId = record.Id,
            Result = record.Result,
            RejectionReason = record.RejectionReason,
            Message = BuildMessage(record),
            MovementType = record.MovementType,
            CredentialCode = record.CredentialCode,
            EmployeeId = record.EmployeeId,
            EmployeeFullName = employee?.FullName,
            AccessPointId = accessPoint.Id,
            AccessPointName = accessPoint.Name,
            OccurredAt = record.OccurredAt
        };
    }

    private static string BuildMessage(AccessRecord record)
    {
        if (record.RejectionReason is RejectionReason reason)
            return $"Acceso rechazado. {reason.ToDescription()}";

        return record.MovementType == MovementType.Entry ? "Entrada autorizada." : "Salida autorizada.";
    }
}
