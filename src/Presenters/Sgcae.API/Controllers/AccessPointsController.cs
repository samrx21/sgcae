using Microsoft.AspNetCore.Mvc;
using Sgcae.Application.UseCases.AccessPoints.Commands.CreateAccessPoint;
using Sgcae.Application.UseCases.AccessPoints.Commands.DisableAccessPoint;
using Sgcae.Application.UseCases.AccessPoints.Commands.EnableAccessPoint;
using Sgcae.Application.UseCases.AccessPoints.Queries;
using Sgcae.Application.UseCases.AccessPoints.Queries.GetAccessPointById;
using Sgcae.Application.UseCases.AccessPoints.Queries.GetAccessPointList;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.API.Controllers;

/// <summary>
/// RF-06. Administración de puntos de acceso: registrar, consultar, habilitar e inhabilitar.
/// </summary>
[ApiController]
[Route("api/access-points")]
[Produces("application/json")]
public sealed class AccessPointsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Lista los puntos de acceso de forma paginada, filtrando por tipo y si están habilitados.
    /// </summary>
    /// <param name="pageNumber">Número de página (mínimo 1).</param>
    /// <param name="pageSize">Puntos por página (máximo 50).</param>
    /// <param name="type">Filtra por tipo: Reception, Parking, Warehouse o RestrictedArea.</param>
    /// <param name="isEnabled">true para ver solo los habilitados; false para los inhabilitados.</param>
    /// <param name="cancellationToken">Token de cancelación de la petición.</param>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<AccessPointDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
        [FromQuery] AccessPointType? type = null,
        [FromQuery] bool? isEnabled = null,
        CancellationToken cancellationToken = default)
    {
        GetAccessPointListQuery query = new()
        {
            Pagination = new PaginationRequest(pageNumber, pageSize),
            Type = type,
            IsEnabled = isEnabled
        };

        PaginationResponse<AccessPointDto> result = await mediator.Send(query, cancellationToken);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Consulta un punto de acceso.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AccessPointDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        AccessPointDto accessPoint = await mediator.Send(new GetAccessPointByIdQuery(id), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, accessPoint);
    }

    /// <summary>
    /// Registra un punto de acceso. Queda habilitado.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateAccessPointCommand command, CancellationToken cancellationToken)
    {
        Guid id = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Habilita un punto de acceso.
    /// </summary>
    [HttpPatch("{id:guid}/enable")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Enable(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new EnableAccessPointCommand(id), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    /// Inhabilita un punto de acceso. Los intentos de acceso por ese punto se rechazan.
    /// </summary>
    [HttpPatch("{id:guid}/disable")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DisableAccessPointCommand(id), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
