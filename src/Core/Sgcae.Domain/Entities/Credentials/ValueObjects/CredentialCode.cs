using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Sgcae.Domain.Exceptions;

namespace Sgcae.Domain.Entities.Credentials.ValueObjects;

/// <summary>
/// Código único de una credencial, con el formato CRD-XXXXXXXX (8 letras o dígitos).
/// Es el dato que se lee en el punto de acceso para identificar al empleado.
/// </summary>
public sealed record CredentialCode
{
    public const string PREFIX = "CRD-";
    public const int RANDOM_PART_LENGTH = 8;
    public const int MAX_LENGTH = 12; // "CRD-" + 8 caracteres

    // Se omiten caracteres que se confunden al leerlos: 0/O, 1/I/L
    private const string ALPHABET = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
    private static readonly Regex CodePattern = new(@"^CRD-[A-Z0-9]{8}$", RegexOptions.Compiled);

    public string Value { get; private set; } = null!;

    // Requerido por EF Core para materializar el objeto
    private CredentialCode() { }

    public CredentialCode(string value)
    {
        string normalized = Normalize(value);

        ApplyCodeRules(normalized);

        Value = normalized;
    }

    /// <summary>
    /// Genera un código nuevo con caracteres aleatorios seguros.
    /// </summary>
    public static CredentialCode Generate()
    {
        char[] randomPart = new char[RANDOM_PART_LENGTH];
        for (int i = 0; i < randomPart.Length; i++)
            randomPart[i] = ALPHABET[RandomNumberGenerator.GetInt32(ALPHABET.Length)];

        return new CredentialCode(PREFIX + new string(randomPart));
    }

    /// <summary>
    /// Deja el código en mayúsculas y sin espacios, como se guarda.
    /// </summary>
    public static string Normalize(string? value) => value?.Trim().ToUpperInvariant() ?? string.Empty;

    private static void ApplyCodeRules(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new BusinessRuleException("El código de la credencial es requerido.");

        if (!CodePattern.IsMatch(code))
            throw new BusinessRuleException(
                "El código de la credencial debe tener el formato CRD- seguido de 8 letras o números.");
    }

    public override string ToString() => Value;
}
