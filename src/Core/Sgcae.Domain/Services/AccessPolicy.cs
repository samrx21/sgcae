using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.AccessRecords;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Exceptions;

namespace Sgcae.Domain.Services;

/// <summary>
/// Servicio de dominio que decide si se autoriza un movimiento. Es un servicio y no un
/// método de una entidad porque la regla involucra tres agregados distintos: la credencial,
/// el empleado y el punto de acceso. No consulta la base de datos: recibe todo lo que necesita.
/// </summary>
public static class AccessPolicy
{
    /// <param name="credential">Credencial encontrada con el código presentado, o null si no existe.</param>
    /// <param name="employee">Empleado dueño de la credencial, o null si no hay credencial.</param>
    /// <param name="accessPoint">Punto de acceso donde se presenta la credencial.</param>
    /// <param name="movement">Movimiento solicitado: entrada o salida.</param>
    /// <param name="lastGrantedMovement">Último movimiento autorizado del empleado, o null si nunca ha ingresado.</param>
    public static AccessDecision Evaluate(
        Credential? credential,
        Employee? employee,
        AccessPoint accessPoint,
        MovementType movement,
        MovementType? lastGrantedMovement)
    {
        if (accessPoint is null)
            throw new BusinessRuleException("El punto de acceso es requerido para evaluar la solicitud.");

        if (!Enum.IsDefined(movement))
            throw new BusinessRuleException("El tipo de movimiento no es válido.");

        // 1. La credencial debe existir y estar activa
        if (credential is null)
            return AccessDecision.Deny(RejectionReason.CredentialNotFound);

        if (credential.Status == CredentialStatus.Revoked)
            return AccessDecision.Deny(RejectionReason.CredentialRevoked);

        if (credential.Status == CredentialStatus.Blocked)
            return AccessDecision.Deny(RejectionReason.CredentialBlocked);

        // 2. El empleado debe tener un vínculo laboral activo
        if (employee is null || !employee.IsActive)
            return AccessDecision.Deny(RejectionReason.EmployeeInactive);

        // 3. El punto de acceso debe estar habilitado
        if (!accessPoint.IsEnabled)
            return AccessDecision.Deny(RejectionReason.AccessPointDisabled);

        // 4. Los movimientos deben alternarse: no dos entradas seguidas, ni salir sin haber entrado.
        //    Esto evita los registros duplicados que tenían las planillas.
        if (movement == MovementType.Entry && lastGrantedMovement == MovementType.Entry)
            return AccessDecision.Deny(RejectionReason.AlreadyInside);

        if (movement == MovementType.Exit && lastGrantedMovement != MovementType.Entry)
            return AccessDecision.Deny(RejectionReason.NotInside);

        return AccessDecision.Grant();
    }
}
