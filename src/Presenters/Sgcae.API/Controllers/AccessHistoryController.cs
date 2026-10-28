using Microsoft.AspNetCore.Mvc;
using Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.API.Controllers;

/// <summary>
/// RF-04. Consulta del historial de accesos.
/// </summary>
[ApiController]
[Route("api/access-history")]
[Produces("application/json")]
public sealed class AccessHistoryController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Consulta el historial de accesos de forma paginada, del más reciente al más antiguo.
    /// Todos los filtros son opcionales y se pueden combinar.
    /// </summary>
    /// <param name="pageNumber">Número de página (mínimo 1).</param>
    /// <param name="pageSize">Movimientos por página (máximo 50).</param>
    /// <param name="employeeId">Filtra por empleado.</param>
    /// <param name="from">Fecha inicial (yyyy-MM-dd, UTC), incluida.</param>
    /// <param name="to">Fecha final (yyyy-MM-dd, UTC), incluida.</param>
    /// <param name="accessPointId">Filtra por punto de acceso.</param>
    /// <param name="result">Filtra por resultado: Granted o Denied.</param>
    /// <param name="cancellationToken">Token de cancelación de la petición.</param>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<AccessHistoryItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] DateOnly? from = null,
        [FromQuery] DateOnly? to = null,
        [FromQuery] Guid? accessPointId = null,
        [FromQuery] AccessResult? result = null,
        CancellationToken cancellationToken = default)
    {
        GetAccessHistoryQuery query = new()
        {
            Pagination = new PaginationRequest(pageNumber, pageSize),
            EmployeeId = employeeId,
            From = from,
            To = to,
            AccessPointId = accessPointId,
            Result = result
        };

        PaginationResponse<AccessHistoryItemDto> history = await mediator.Send(query, cancellationToken);

        return StatusCode(StatusCodes.Status200OK, history);
    }
}
