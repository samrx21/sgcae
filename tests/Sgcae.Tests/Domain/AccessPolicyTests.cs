using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.AccessRecords;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Services;

namespace Sgcae.Tests.Domain;

/// <summary>
/// Pruebas de la regla central del RF-03: cuándo se autoriza y cuándo se rechaza un acceso.
/// </summary>
public class AccessPolicyTests
{
    private readonly Employee _employee = TestData.ActiveEmployee();
    private readonly Credential _credential = TestData.ActiveCredential();
    private readonly AccessPoint _accessPoint = TestData.EnabledAccessPoint();

    [Fact]
    public void Evaluate_FirstEntry_IsGranted()
    {
        AccessDecision decision = AccessPolicy.Evaluate(_credential, _employee, _accessPoint, MovementType.Entry, null);

        Assert.True(decision.IsGranted);
        Assert.Null(decision.Reason);
    }

    [Fact]
    public void Evaluate_ExitAfterEntry_IsGranted()
    {
        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, _employee, _accessPoint, MovementType.Exit, MovementType.Entry);

        Assert.True(decision.IsGranted);
    }

    [Fact]
    public void Evaluate_EntryAfterExit_IsGranted()
    {
        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, _employee, _accessPoint, MovementType.Entry, MovementType.Exit);

        Assert.True(decision.IsGranted);
    }

    [Fact]
    public void Evaluate_UnknownCredential_IsDenied()
    {
        AccessDecision decision = AccessPolicy.Evaluate(null, null, _accessPoint, MovementType.Entry, null);

        AssertDenied(decision, RejectionReason.CredentialNotFound);
    }

    [Fact]
    public void Evaluate_BlockedCredential_IsDenied()
    {
        _credential.Block("Reporte de pérdida");

        AccessDecision decision = AccessPolicy.Evaluate(_credential, _employee, _accessPoint, MovementType.Entry, null);

        AssertDenied(decision, RejectionReason.CredentialBlocked);
    }

    [Fact]
    public void Evaluate_RevokedCredential_IsDenied()
    {
        _credential.Revoke("Terminación del contrato");

        AccessDecision decision = AccessPolicy.Evaluate(_credential, _employee, _accessPoint, MovementType.Entry, null);

        AssertDenied(decision, RejectionReason.CredentialRevoked);
    }

    [Fact]
    public void Evaluate_InactiveEmployee_IsDenied()
    {
        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, TestData.InactiveEmployee(), _accessPoint, MovementType.Entry, null);

        AssertDenied(decision, RejectionReason.EmployeeInactive);
    }

    [Fact]
    public void Evaluate_DisabledAccessPoint_IsDenied()
    {
        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, _employee, TestData.DisabledAccessPoint(), MovementType.Entry, null);

        AssertDenied(decision, RejectionReason.AccessPointDisabled);
    }

    [Fact]
    public void Evaluate_SecondEntryWithoutExit_IsDenied()
    {
        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, _employee, _accessPoint, MovementType.Entry, MovementType.Entry);

        AssertDenied(decision, RejectionReason.AlreadyInside);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(MovementType.Exit)]
    public void Evaluate_ExitWithoutPreviousEntry_IsDenied(MovementType? lastGrantedMovement)
    {
        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, _employee, _accessPoint, MovementType.Exit, lastGrantedMovement);

        AssertDenied(decision, RejectionReason.NotInside);
    }

    [Fact]
    public void Evaluate_CredentialIsCheckedBeforeEmployee()
    {
        // Si fallan varias condiciones, se informa la primera: la credencial
        _credential.Block("Reporte de pérdida");

        AccessDecision decision = AccessPolicy.Evaluate(
            _credential, TestData.InactiveEmployee(), TestData.DisabledAccessPoint(), MovementType.Entry, null);

        AssertDenied(decision, RejectionReason.CredentialBlocked);
    }

    private static void AssertDenied(AccessDecision decision, RejectionReason expectedReason)
    {
        Assert.False(decision.IsGranted);
        Assert.Equal(AccessResult.Denied, decision.Result);
        Assert.Equal(expectedReason, decision.Reason);
    }
}
