using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Credentials.ValueObjects;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Persistence.Configurations;

/// <summary>
/// Mapeo de la entidad Credential a la tabla Credentials.
/// </summary>
internal sealed class CredentialConfiguration : IEntityTypeConfiguration<Credential>
{
    public void Configure(EntityTypeBuilder<Credential> builder)
    {
        builder.ToTable("Credentials");
        builder.HasKey(credential => credential.Id);
        builder.Property(credential => credential.Id).ValueGeneratedNever();

        builder.Property(credential => credential.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Property(credential => credential.StatusReason).HasMaxLength(Credential.REASON_MAX_LENGTH);
        builder.Property(credential => credential.IssuedAt)
            .HasConversion(value => value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();
        builder.Property(credential => credential.StatusChangedAt)
            .HasConversion(
                value => value,
                value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value);
        builder.Ignore(credential => credential.CanBeUsed);

        builder.OwnsOne(credential => credential.Code, code =>
        {
            code.Property(value => value.Value)
                .HasColumnName("Code")
                .HasMaxLength(CredentialCode.MAX_LENGTH)
                .IsRequired();
            code.HasIndex(value => value.Value).IsUnique();
        });
        builder.Navigation(credential => credential.Code).IsRequired();

        // Clave foránea hacia el empleado sin propiedad de navegación: entre agregados
        // solo se guarda el Id. Restrict impide borrar un empleado que tenga credenciales.
        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(credential => credential.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(credential => new { credential.EmployeeId, credential.Status });
    }
}
