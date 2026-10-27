using Sgcae.Application.UseCases.AccessPoints.Queries;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Application.UseCases.AccessPoints;

/// <summary>
/// Transformación de la entidad AccessPoint hacia su DTO.
/// </summary>
internal static class AccessPointMapperExtensions
{
    public static AccessPointDto ToDto(this AccessPoint accessPoint) =>
        new()
        {
            Id = accessPoint.Id,
            Name = accessPoint.Name,
            Type = accessPoint.Type,
            Location = accessPoint.Location,
            IsEnabled = accessPoint.IsEnabled,
            CreatedAt = accessPoint.CreatedAt,
            UpdatedAt = accessPoint.UpdatedAt
        };
}
