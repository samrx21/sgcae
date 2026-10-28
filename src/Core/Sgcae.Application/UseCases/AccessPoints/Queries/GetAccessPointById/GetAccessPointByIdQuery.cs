using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.AccessPoints.Queries.GetAccessPointById;

/// <summary>
/// Query para consultar un punto de acceso por su Id.
/// </summary>
public sealed record GetAccessPointByIdQuery(Guid Id) : IRequest<AccessPointDto>;
