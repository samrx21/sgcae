using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgcae.Domain.Entities.AccessPoints;

namespace Sgcae.Persistence.Configurations;

/// <summary>
/// Mapeo de la entidad AccessPoint a la tabla AccessPoints, con los puntos de acceso
/// iniciales de la sede como datos semilla.
/// </summary>
internal sealed class AccessPointConfiguration : IEntityTypeConfiguration<AccessPoint>
{
    private static readonly DateTime SeedDate = new(2026, 10, 27, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<AccessPoint> builder)
    {
        builder.ToTable("AccessPoints");
        builder.HasKey(accessPoint => accessPoint.Id);
        builder.Property(accessPoint => accessPoint.Id).ValueGeneratedNever();

        builder.Property(accessPoint => accessPoint.Name).HasMaxLength(AccessPoint.NAME_MAX_LENGTH).IsRequired();
        builder.HasIndex(accessPoint => accessPoint.Name).IsUnique();
        builder.Property(accessPoint => accessPoint.Type).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(accessPoint => accessPoint.Location).HasMaxLength(AccessPoint.LOCATION_MAX_LENGTH);
        builder.Property(accessPoint => accessPoint.IsEnabled).IsRequired();
        builder.Property(accessPoint => accessPoint.CreatedAt)
            .HasConversion(value => value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();
        builder.Property(accessPoint => accessPoint.UpdatedAt)
            .HasConversion(
                value => value,
                value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value);

        // Puntos de acceso que ya existen en la sede (Ids fijos para que la migración sea estable)
        builder.HasData(
            Seed("0199f7a0-0000-7000-8000-000000000001", "Recepción principal", AccessPointType.Reception, "Primer piso, entrada principal"),
            Seed("0199f7a0-0000-7000-8000-000000000002", "Parqueadero", AccessPointType.Parking, "Sótano, acceso vehicular"),
            Seed("0199f7a0-0000-7000-8000-000000000003", "Bodega central", AccessPointType.Warehouse, "Zona de carga y descarga"),
            Seed("0199f7a0-0000-7000-8000-000000000004", "Sala de servidores", AccessPointType.RestrictedArea, "Segundo piso, área de tecnología"));
    }

    private static object Seed(string id, string name, AccessPointType type, string location) =>
        new
        {
            Id = Guid.Parse(id),
            Name = name,
            Type = type,
            Location = location,
            IsEnabled = true,
            CreatedAt = SeedDate
        };
}
