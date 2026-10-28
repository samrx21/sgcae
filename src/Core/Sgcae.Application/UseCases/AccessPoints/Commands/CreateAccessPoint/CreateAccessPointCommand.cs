using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Commands.CreateAccessPoint;

/// <summary>
/// Comando para registrar un punto de acceso. Se crea habilitado y devuelve su Id.
/// </summary>
public sealed record CreateAccessPointCommand(string Name, AccessPointType Type, string? Location) : IRequest<Guid>;
