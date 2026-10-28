using Microsoft.AspNetCore.Mvc;
using Sgcae.API.Requests.Employees;
using Sgcae.Application.UseCases.Employees.Commands.CreateEmployee;
using Sgcae.Application.UseCases.Employees.Commands.DeactivateEmployee;
using Sgcae.Application.UseCases.Employees.Commands.UpdateEmployee;
using Sgcae.Application.UseCases.Employees.Queries.GetEmployeeById;
using Sgcae.Application.UseCases.Employees.Queries.GetEmployeeList;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.API.Controllers;

/// <summary>
/// RF-01. Gestión de empleados: registrar, consultar, actualizar y desactivar.
/// </summary>
[ApiController]
[Route("api/employees")]
[Produces("application/json")]
public sealed class EmployeesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Lista los empleados de forma paginada, con búsqueda y filtros.
    /// </summary>
    /// <param name="pageNumber">Número de página (mínimo 1).</param>
    /// <param name="pageSize">Empleados por página (máximo 50).</param>
    /// <param name="search">Texto a buscar en nombre, apellido, identificación o correo.</param>
    /// <param name="status">Filtra por estado laboral: Active o Inactive.</param>
    /// <param name="area">Filtra por área organizacional.</param>
    /// <param name="cancellationToken">Token de cancelación de la petición.</param>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<EmployeeListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
        [FromQuery] string? search = null,
        [FromQuery] EmploymentStatus? status = null,
        [FromQuery] string? area = null,
        CancellationToken cancellationToken = default)
    {
        GetEmployeeListQuery query = new()
        {
            Pagination = new PaginationRequest(pageNumber, pageSize),
            Search = search,
            Status = status,
            Area = area
        };

        PaginationResponse<EmployeeListItemDto> result = await mediator.Send(query, cancellationToken);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Consulta el detalle de un empleado.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EmployeeDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        EmployeeDetailDto employee = await mediator.Send(new GetEmployeeByIdQuery(id), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, employee);
    }

    /// <summary>
    /// Registra un empleado. Queda activo.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Actualiza los datos de un empleado activo. La identificación no se puede cambiar.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id, [FromBody] UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        UpdateEmployeeCommand command = new(
            id, request.FirstName, request.LastName, request.Email, request.JobTitle, request.Area);

        await mediator.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    /// Desactiva el vínculo laboral del empleado. Desde ese momento se le niega el acceso.
    /// </summary>
    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeactivateEmployeeCommand(id), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
