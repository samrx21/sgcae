using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints.Queries;

/// <summary>
/// Datos de un punto de acceso. Se usa tanto en el detalle como en el listado,
/// porque la entidad tiene pocos campos.
/// </summary>
public sealed class AccessPointDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public AccessPointType Type { get; init; }
    public string? Location { get; init; }
    public bool IsEnabled { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
