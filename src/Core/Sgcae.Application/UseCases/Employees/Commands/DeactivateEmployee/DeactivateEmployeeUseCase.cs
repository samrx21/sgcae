using Sgcae.Application.Contracts.Persistence;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Commands.DeactivateEmployee;

public sealed class DeactivateEmployeeUseCase(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeactivateEmployeeCommand>
{
    public async Task Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"No existe un empleado con el Id {request.Id}.");

        employee.Deactivate();

        await employeeRepository.UpdateAsync(employee, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
