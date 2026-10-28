using Microsoft.AspNetCore.Mvc;
using Sgcae.API.Requests.Credentials;
using Sgcae.Application.UseCases.Credentials.Commands.AssignCredential;
using Sgcae.Application.UseCases.Credentials.Commands.BlockCredential;
using Sgcae.Application.UseCases.Credentials.Commands.RevokeCredential;
using Sgcae.Application.UseCases.Credentials.Commands.UnblockCredential;
using Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;
using Sgcae.Application.UseCases.Credentials.Queries.GetCredentialList;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.API.Controllers;

/// <summary>
/// RF-02. Gestión de credenciales: asignar, consultar, bloquear, desbloquear y revocar.
/// </summary>
[ApiController]
[Route("api/credentials")]
[Produces("application/json")]
public sealed class CredentialsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Lista las credenciales de forma paginada, filtrando por empleado y estado.
    /// </summary>
    /// <param name="pageNumber">Número de página (mínimo 1).</param>
    /// <param name="pageSize">Credenciales por página (máximo 50).</param>
    /// <param name="employeeId">Filtra por empleado.</param>
    /// <param name="status">Filtra por estado: Active, Blocked o Revoked.</param>
    /// <param name="cancellationToken">Token de cancelación de la petición.</param>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<CredentialListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] CredentialStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        GetCredentialListQuery query = new()
        {
            Pagination = new PaginationRequest(pageNumber, pageSize),
            EmployeeId = employeeId,
            Status = status
        };

        PaginationResponse<CredentialListItemDto> result = await mediator.Send(query, cancellationToken);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Consulta una credencial y su estado por su código.
    /// </summary>
    [HttpGet("{code}")]
    [ProducesResponseType(typeof(CredentialDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCode(string code, CancellationToken cancellationToken)
    {
        CredentialDetailDto credential = await mediator.Send(new GetCredentialByCodeQuery(code), cancellationToken);

        return StatusCode(StatusCodes.Status200OK, credential);
    }

    /// <summary>
    /// Asigna una credencial nueva a un empleado activo. El código lo genera el sistema.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CredentialDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Assign([FromBody] AssignCredentialCommand command, CancellationToken cancellationToken)
    {
        CredentialDetailDto credential = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetByCode), new { code = credential.Code }, credential);
    }

    /// <summary>
    /// Bloquea temporalmente una credencial, indicando el motivo.
    /// </summary>
    [HttpPatch("{code}/block")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Block(
        string code, [FromBody] CredentialStatusChangeRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new BlockCredentialCommand(code, request.Reason), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    /// Vuelve a activar una credencial bloqueada.
    /// </summary>
    [HttpPatch("{code}/unblock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unblock(string code, CancellationToken cancellationToken)
    {
        await mediator.Send(new UnblockCredentialCommand(code), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    /// Revoca una credencial de forma definitiva, indicando el motivo.
    /// </summary>
    [HttpPatch("{code}/revoke")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Revoke(
        string code, [FromBody] CredentialStatusChangeRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new RevokeCredentialCommand(code, request.Reason), cancellationToken);

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
