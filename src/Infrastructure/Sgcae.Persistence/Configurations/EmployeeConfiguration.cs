using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgcae.Domain.Common.ValueObjects;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Entities.Employees.ValueObjects;

namespace Sgcae.Persistence.Configurations;

/// <summary>
/// Mapeo de la entidad Employee a la tabla Employees. Los value objects se guardan
/// como columnas de la misma tabla (owned types).
/// </summary>
internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(employee => employee.Id);
        builder.Property(employee => employee.Id).ValueGeneratedNever();

        builder.Property(employee => employee.FirstName).HasMaxLength(Employee.NAME_MAX_LENGTH).IsRequired();
        builder.Property(employee => employee.LastName).HasMaxLength(Employee.NAME_MAX_LENGTH).IsRequired();
        builder.Property(employee => employee.JobTitle).HasMaxLength(Employee.JOB_TITLE_MAX_LENGTH).IsRequired();
        builder.Property(employee => employee.Area).HasMaxLength(Employee.AREA_MAX_LENGTH).IsRequired();
        builder.Property(employee => employee.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.HasIndex(employee => employee.Area);

        // SQL Server no guarda la zona horaria: al leer, se marcan las fechas como UTC
        builder.Property(employee => employee.CreatedAt)
            .HasConversion(value => value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();
        builder.Property(employee => employee.UpdatedAt)
            .HasConversion(
                value => value,
                value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value);

        builder.Ignore(employee => employee.FullName);
        builder.Ignore(employee => employee.IsActive);

        builder.OwnsOne(employee => employee.Identification, identification =>
        {
            identification.Property(value => value.Value)
                .HasColumnName("IdentificationNumber")
                .HasMaxLength(IdentificationNumber.MAX_LENGTH)
                .IsRequired();
            identification.HasIndex(value => value.Value).IsUnique();
        });
        builder.Navigation(employee => employee.Identification).IsRequired();

        builder.OwnsOne(employee => employee.Email, email =>
        {
            email.Property(value => value.Value)
                .HasColumnName("Email")
                .HasMaxLength(Email.MAX_LENGTH)
                .IsRequired();
            email.HasIndex(value => value.Value).IsUnique();
        });
        builder.Navigation(employee => employee.Email).IsRequired();
    }
}
