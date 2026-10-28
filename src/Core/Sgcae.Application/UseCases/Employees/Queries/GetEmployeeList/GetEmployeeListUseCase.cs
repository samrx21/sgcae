using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Employees.Queries.GetEmployeeList;

public sealed class GetEmployeeListUseCase(IEmployeeRepository employeeRepository)
    : IRequestHandler<GetEmployeeListQuery, PaginationResponse<EmployeeListItemDto>>
{
    public async Task<PaginationResponse<EmployeeListItemDto>> Handle(
        GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        PaginationResponse<Employee> page = await employeeRepository.GetPagedListAsync(
            request.Pagination, request.Search, request.Status, request.Area, cancellationToken);

        List<EmployeeListItemDto> items = page.Items.Select(employee => employee.ToListItemDto()).ToList();

        return PaginationResponse<EmployeeListItemDto>.Create(items, page.TotalCount, request.Pagination);
    }
}
