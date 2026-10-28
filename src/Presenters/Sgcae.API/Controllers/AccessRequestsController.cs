using Microsoft.AspNetCore.Mvc;
using Sgcae.Application.UseCases.AccessRecords.Commands.RegisterAccess;
using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.API.Controllers;

/// <summary>
/// RF-03. Registro de entradas y salidas: recibe las solicitudes de los puntos de acceso.
/// </summary>
[ApiController]
[Route("api/access-requests")]
[Produces("application/json")]
public sealed class AccessRequestsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Valida una solicitud de entrada o salida y registra su resultado.
    /// </summary>
    /// <remarks>
    /// La respuesta es 201 tanto si el acceso se autoriza como si se rechaza, porque en ambos
    /// casos queda un registro en el historial. El campo "result" indica la decisión.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(AccessRegistrationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] RegisterAccessCommand command, CancellationToken cancellationToken)
    {
        AccessRegistrationDto result = await mediator.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}
