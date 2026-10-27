using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.AccessPoints.Commands.EnableAccessPoint;

/// <summary>
/// Comando para habilitar un punto de acceso.
/// </summary>
public sealed record EnableAccessPointCommand(Guid Id) : IRequest;
