using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.AccessPoints.Commands.DisableAccessPoint;

/// <summary>
/// Comando para inhabilitar un punto de acceso, por ejemplo por mantenimiento.
/// </summary>
public sealed record DisableAccessPointCommand(Guid Id) : IRequest;
