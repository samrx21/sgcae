using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Queries.GetEmployeeById;

public sealed class GetEmployeeByIdUseCase(IEmployeeRepository employeeRepository)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailDto>
{
    public async Task<EmployeeDetailDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"No existe un empleado con el Id {request.Id}.");

        return employee.ToDetailDto();
    }
}
