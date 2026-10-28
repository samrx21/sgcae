using Sgcae.Domain.Exceptions;

namespace Sgcae.Domain.Entities.Employees.ValueObjects;

/// <summary>
/// Número de identificación del empleado (cédula). Solo admite dígitos y no cambia
/// durante la vida del empleado en la empresa.
/// </summary>
public sealed record IdentificationNumber
{
    public const int MIN_LENGTH = 5;
    public const int MAX_LENGTH = 15;

    public string Value { get; private set; } = null!;

    // Requerido por EF Core para materializar el objeto
    private IdentificationNumber() { }

    public IdentificationNumber(string value)
    {
        string normalized = value?.Trim() ?? string.Empty;

        ApplyIdentificationRules(normalized);

        Value = normalized;
    }

    private static void ApplyIdentificationRules(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessRuleException("El número de identificación es requerido.");

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            throw new BusinessRuleException(
                $"El número de identificación debe tener entre {MIN_LENGTH} y {MAX_LENGTH} dígitos.");

        if (!value.All(char.IsDigit))
            throw new BusinessRuleException("El número de identificación solo puede contener números.");
    }

    public override string ToString() => Value;
}
