using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.AccessRecords;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Persistence.Configurations;

/// <summary>
/// Mapeo de la entidad AccessRecord a la tabla AccessRecords, con índices pensados para
/// las consultas del historial (por empleado, punto de acceso, fecha y resultado).
/// </summary>
internal sealed class AccessRecordConfiguration : IEntityTypeConfiguration<AccessRecord>
{
    public void Configure(EntityTypeBuilder<AccessRecord> builder)
    {
        builder.ToTable("AccessRecords");
        builder.HasKey(record => record.Id);
        builder.Property(record => record.Id).ValueGeneratedNever();

        builder.Property(record => record.CredentialCode)
            .HasMaxLength(AccessRecord.PRESENTED_CODE_MAX_LENGTH)
            .IsRequired();
        builder.Property(record => record.MovementType).HasConversion<string>().HasMaxLength(10).IsRequired();
        builder.Property(record => record.Result).HasConversion<string>().HasMaxLength(10).IsRequired();
        builder.Property(record => record.RejectionReason).HasConversion<string>().HasMaxLength(30);
        builder.Property(record => record.OccurredAt)
            .HasConversion(value => value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();

        // Claves foráneas sin navegación. La credencial y el empleado son opcionales porque
        // un intento con un código inexistente también se registra.
        builder.HasOne<AccessPoint>()
            .WithMany()
            .HasForeignKey(record => record.AccessPointId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Credential>()
            .WithMany()
            .HasForeignKey(record => record.CredentialId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(record => record.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(record => record.OccurredAt);
        builder.HasIndex(record => new { record.EmployeeId, record.OccurredAt });
        builder.HasIndex(record => new { record.AccessPointId, record.OccurredAt });
        builder.HasIndex(record => record.Result);
    }
}
