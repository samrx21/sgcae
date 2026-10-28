using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Common.ValueObjects;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Entities.Employees.ValueObjects;

namespace Sgcae.Application.UseCases.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeUseCase(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Los value objects validan su propio formato al construirse
        IdentificationNumber identification = new(request.IdentificationNumber);
        Email email = new(request.Email);

        // La unicidad depende de los datos guardados, por eso se valida aquí y no en el dominio
        if (await employeeRepository.ExistsByIdentificationAsync(identification.Value, cancellationToken))
            throw new ConflictException(
                $"Ya existe un empleado registrado con la identificación {identification.Value}.");

        if (await employeeRepository.ExistsByEmailAsync(email.Value, cancellationToken: cancellationToken))
            throw new ConflictException($"Ya existe un empleado registrado con el correo {email.Value}.");

        Employee employee = new(
            identification, request.FirstName, request.LastName, email, request.JobTitle, request.Area);

        await employeeRepository.AddAsync(employee, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return employee.Id;
    }
}
