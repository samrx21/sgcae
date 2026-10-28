using Sgcae.Domain.Common.ValueObjects;
using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Credentials.ValueObjects;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Entities.Employees.ValueObjects;

namespace Sgcae.Tests.Domain;

/// <summary>
/// Objetos válidos de partida para las pruebas. Cada prueba cambia solo lo que necesita.
/// </summary>
internal static class TestData
{
    public static Employee ActiveEmployee() =>
        new(new IdentificationNumber("1037654321"), "Laura", "Gómez",
            new Email("laura.gomez@logistica.com"), "Analista de inventarios", "Bodega");

    public static Employee InactiveEmployee()
    {
        Employee employee = ActiveEmployee();
        employee.Deactivate();
        return employee;
    }

    public static Credential ActiveCredential() => Credential.Issue(ActiveEmployee(), CredentialCode.Generate());

    public static AccessPoint EnabledAccessPoint() => new("Recepción principal", AccessPointType.Reception, "Primer piso");

    public static AccessPoint DisabledAccessPoint()
    {
        AccessPoint accessPoint = EnabledAccessPoint();
        accessPoint.Disable();
        return accessPoint;
    }
}
