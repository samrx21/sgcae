using Sgcae.Domain.Entities.Credentials.ValueObjects;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Exceptions;

namespace Sgcae.Domain.Entities.Credentials;

/// <summary>
/// Credencial de acceso asignada a un empleado. Es un agregado distinto al empleado:
/// lo referencia solo por su Id, y su estado determina si puede usarse en un punto de acceso.
/// </summary>
public sealed class Credential
{
    public const int REASON_MIN_LENGTH = 5;
    public const int REASON_MAX_LENGTH = 200;

    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public CredentialCode Code { get; private set; } = null!;
    public CredentialStatus Status { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public DateTime? StatusChangedAt { get; private set; }
    public string? StatusReason { get; private set; }

    /// <summary>
    /// Una credencial solo sirve para entrar o salir si está activa.
    /// </summary>
    public bool CanBeUsed => Status == CredentialStatus.Active;

    // Requerido por EF Core para materializar la entidad
    private Credential() { }

    private Credential(Guid employeeId, CredentialCode code)
    {
        Id = Guid.CreateVersion7();
        EmployeeId = employeeId;
        Code = code;
        Status = CredentialStatus.Active;
        IssuedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Emite una credencial activa para el empleado, con el código indicado.
    /// Que el empleado no tenga otra credencial vigente lo valida la capa de aplicación,
    /// porque depende de las credenciales ya guardadas.
    /// </summary>
    public static Credential Issue(Employee employee, CredentialCode code)
    {
        if (employee is null)
            throw new BusinessRuleException("El empleado de la credencial es requerido.");

        if (!employee.IsActive)
            throw new BusinessRuleException("No se puede asignar una credencial a un empleado inactivo.");

        if (code is null)
            throw new BusinessRuleException("El código de la credencial es requerido.");

        // Solo se guarda el Id: cada agregado referencia a los demás por su identidad
        return new Credential(employee.Id, code);
    }

    /// <summary>
    /// Bloquea temporalmente la credencial, por ejemplo por pérdida o un incidente.
    /// </summary>
    public void Block(string reason)
    {
        string normalizedReason = ApplyReasonRules(reason);

        if (Status == CredentialStatus.Revoked)
            throw new BusinessRuleException("No se puede bloquear una credencial revocada.");

        if (Status == CredentialStatus.Blocked)
            throw new BusinessRuleException("La credencial ya se encuentra bloqueada.");

        ChangeStatus(CredentialStatus.Blocked, normalizedReason);
    }

    /// <summary>
    /// Vuelve a activar una credencial bloqueada.
    /// </summary>
    public void Unblock()
    {
        if (Status != CredentialStatus.Blocked)
            throw new BusinessRuleException("Solo se puede desbloquear una credencial bloqueada.");

        ChangeStatus(CredentialStatus.Active, null);
    }

    /// <summary>
    /// Anula la credencial de forma definitiva. Una credencial revocada no se puede reactivar.
    /// </summary>
    public void Revoke(string reason)
    {
        string normalizedReason = ApplyReasonRules(reason);

        if (Status == CredentialStatus.Revoked)
            throw new BusinessRuleException("La credencial ya se encuentra revocada.");

        ChangeStatus(CredentialStatus.Revoked, normalizedReason);
    }

    private void ChangeStatus(CredentialStatus status, string? reason)
    {
        Status = status;
        StatusReason = reason;
        StatusChangedAt = DateTime.UtcNow;
    }

    private static string ApplyReasonRules(string reason)
    {
        string normalized = reason?.Trim() ?? string.Empty;

        if (normalized.Length < REASON_MIN_LENGTH || normalized.Length > REASON_MAX_LENGTH)
            throw new BusinessRuleException(
                $"El motivo debe tener entre {REASON_MIN_LENGTH} y {REASON_MAX_LENGTH} caracteres.");

        return normalized;
    }
}
