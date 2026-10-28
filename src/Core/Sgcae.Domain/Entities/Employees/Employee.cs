using Sgcae.Domain.Common.ValueObjects;
using Sgcae.Domain.Entities.Employees.ValueObjects;
using Sgcae.Domain.Exceptions;

namespace Sgcae.Domain.Entities.Employees;

/// <summary>
/// Empleado de la organización. Es la raíz de su agregado: los datos solo cambian
/// por sus métodos, que validan las reglas de negocio.
/// </summary>
public sealed class Employee
{
    public const int NAME_MIN_LENGTH = 2;
    public const int NAME_MAX_LENGTH = 64;
    public const int JOB_TITLE_MAX_LENGTH = 80;
    public const int AREA_MAX_LENGTH = 80;

    public Guid Id { get; private set; }
    public IdentificationNumber Identification { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string JobTitle { get; private set; } = null!;
    public string Area { get; private set; } = null!;
    public EmploymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public string FullName => $"{FirstName} {LastName}";
    public bool IsActive => Status == EmploymentStatus.Active;

    // Requerido por EF Core para materializar la entidad
    private Employee() { }

    public Employee(
        IdentificationNumber identification,
        string firstName,
        string lastName,
        Email email,
        string jobTitle,
        string area)
    {
        ApplyIdentificationRules(identification);
        string normalizedFirstName = ApplyNameRules(firstName, "nombre");
        string normalizedLastName = ApplyNameRules(lastName, "apellido");
        ApplyEmailRules(email);
        string normalizedJobTitle = ApplyTextRules(jobTitle, "cargo", JOB_TITLE_MAX_LENGTH);
        string normalizedArea = ApplyTextRules(area, "área organizacional", AREA_MAX_LENGTH);

        Id = Guid.CreateVersion7();
        Identification = identification;
        FirstName = normalizedFirstName;
        LastName = normalizedLastName;
        Email = email;
        JobTitle = normalizedJobTitle;
        Area = normalizedArea;
        Status = EmploymentStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Actualiza los datos del empleado. La identificación no se puede cambiar.
    /// </summary>
    public void UpdateInformation(string firstName, string lastName, Email email, string jobTitle, string area)
    {
        EnsureIsActive();
        string normalizedFirstName = ApplyNameRules(firstName, "nombre");
        string normalizedLastName = ApplyNameRules(lastName, "apellido");
        ApplyEmailRules(email);
        string normalizedJobTitle = ApplyTextRules(jobTitle, "cargo", JOB_TITLE_MAX_LENGTH);
        string normalizedArea = ApplyTextRules(area, "área organizacional", AREA_MAX_LENGTH);

        FirstName = normalizedFirstName;
        LastName = normalizedLastName;
        Email = email;
        JobTitle = normalizedJobTitle;
        Area = normalizedArea;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Desactiva el vínculo laboral. Un empleado inactivo no puede ingresar a las instalaciones.
    /// </summary>
    public void Deactivate()
    {
        if (Status == EmploymentStatus.Inactive)
            throw new BusinessRuleException("El empleado ya se encuentra inactivo.");

        Status = EmploymentStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    private void EnsureIsActive()
    {
        if (Status == EmploymentStatus.Inactive)
            throw new BusinessRuleException("No se pueden modificar los datos de un empleado inactivo.");
    }

    private static void ApplyIdentificationRules(IdentificationNumber identification)
    {
        if (identification is null)
            throw new BusinessRuleException("El número de identificación es requerido.");
    }

    private static void ApplyEmailRules(Email email)
    {
        if (email is null)
            throw new BusinessRuleException("El correo electrónico es requerido.");
    }

    private static string ApplyNameRules(string name, string field)
    {
        string normalized = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalized))
            throw new BusinessRuleException($"El {field} es requerido.");

        if (normalized.Length < NAME_MIN_LENGTH || normalized.Length > NAME_MAX_LENGTH)
            throw new BusinessRuleException(
                $"El {field} debe tener entre {NAME_MIN_LENGTH} y {NAME_MAX_LENGTH} caracteres.");

        return normalized;
    }

    private static string ApplyTextRules(string value, string field, int maxLength)
    {
        string normalized = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalized))
            throw new BusinessRuleException($"El {field} es requerido.");

        if (normalized.Length > maxLength)
            throw new BusinessRuleException($"El {field} no puede superar {maxLength} caracteres.");

        return normalized;
    }
}
