using Sgcae.Domain.Exceptions;

namespace Sgcae.Domain.Entities.AccessPoints;

/// <summary>
/// Punto de acceso físico de la organización (recepción, parqueadero, bodega, área restringida).
/// Solo los puntos habilitados permiten registrar entradas y salidas.
/// </summary>
public sealed class AccessPoint
{
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 80;
    public const int LOCATION_MAX_LENGTH = 200;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public AccessPointType Type { get; private set; }
    public string? Location { get; private set; }
    public bool IsEnabled { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Requerido por EF Core para materializar la entidad
    private AccessPoint() { }

    public AccessPoint(string name, AccessPointType type, string? location)
    {
        string normalizedName = ApplyNameRules(name);
        ApplyTypeRules(type);
        string? normalizedLocation = ApplyLocationRules(location);

        Id = Guid.CreateVersion7();
        Name = normalizedName;
        Type = type;
        Location = normalizedLocation;
        IsEnabled = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Enable()
    {
        if (IsEnabled)
            throw new BusinessRuleException("El punto de acceso ya se encuentra habilitado.");

        IsEnabled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Inhabilita el punto, por ejemplo por mantenimiento. Los intentos de acceso
    /// por un punto inhabilitado se registran como rechazados.
    /// </summary>
    public void Disable()
    {
        if (!IsEnabled)
            throw new BusinessRuleException("El punto de acceso ya se encuentra inhabilitado.");

        IsEnabled = false;
        UpdatedAt = DateTime.UtcNow;
    }

    private static string ApplyNameRules(string name)
    {
        string normalized = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalized))
            throw new BusinessRuleException("El nombre del punto de acceso es requerido.");

        if (normalized.Length < NAME_MIN_LENGTH || normalized.Length > NAME_MAX_LENGTH)
            throw new BusinessRuleException(
                $"El nombre del punto de acceso debe tener entre {NAME_MIN_LENGTH} y {NAME_MAX_LENGTH} caracteres.");

        return normalized;
    }

    private static void ApplyTypeRules(AccessPointType type)
    {
        if (!Enum.IsDefined(type))
            throw new BusinessRuleException("El tipo de punto de acceso no es válido.");
    }

    private static string? ApplyLocationRules(string? location)
    {
        string? normalized = string.IsNullOrWhiteSpace(location) ? null : location.Trim();

        if (normalized is not null && normalized.Length > LOCATION_MAX_LENGTH)
            throw new BusinessRuleException(
                $"La ubicación no puede superar {LOCATION_MAX_LENGTH} caracteres.");

        return normalized;
    }
}
