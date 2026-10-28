using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Common.ValueObjects;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Commands.UpdateEmployee;

public sealed class UpdateEmployeeUseCase(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEmployeeCommand>
{
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"No existe un empleado con el Id {request.Id}.");

        Email email = new(request.Email);

        if (await employeeRepository.ExistsByEmailAsync(email.Value, employee.Id, cancellationToken))
            throw new ConflictException($"Ya existe otro empleado registrado con el correo {email.Value}.");

        employee.UpdateInformation(request.FirstName, request.LastName, email, request.JobTitle, request.Area);

        await employeeRepository.UpdateAsync(employee, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
