using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Credentials.ValueObjects;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Exceptions;

namespace Sgcae.Tests.Domain;

/// <summary>
/// Pruebas del RF-02: emisión de credenciales y sus cambios de estado.
/// </summary>
public class CredentialTests
{
    [Fact]
    public void Issue_ForActiveEmployee_CreatesActiveCredential()
    {
        Employee employee = TestData.ActiveEmployee();

        Credential credential = Credential.Issue(employee, CredentialCode.Generate());

        Assert.Equal(employee.Id, credential.EmployeeId);
        Assert.Equal(CredentialStatus.Active, credential.Status);
        Assert.True(credential.CanBeUsed);
    }

    [Fact]
    public void Issue_ForInactiveEmployee_Throws()
    {
        BusinessRuleException exception = Assert.Throws<BusinessRuleException>(
            () => Credential.Issue(TestData.InactiveEmployee(), CredentialCode.Generate()));

        Assert.Contains("inactivo", exception.Message);
    }

    [Fact]
    public void Block_ThenUnblock_ReturnsToActive()
    {
        Credential credential = TestData.ActiveCredential();

        credential.Block("Reporte de pérdida");
        Assert.Equal(CredentialStatus.Blocked, credential.Status);
        Assert.False(credential.CanBeUsed);
        Assert.Equal("Reporte de pérdida", credential.StatusReason);

        credential.Unblock();
        Assert.Equal(CredentialStatus.Active, credential.Status);
        Assert.Null(credential.StatusReason);
    }

    [Fact]
    public void Block_AlreadyBlocked_Throws()
    {
        Credential credential = TestData.ActiveCredential();
        credential.Block("Reporte de pérdida");

        Assert.Throws<BusinessRuleException>(() => credential.Block("Otro motivo válido"));
    }

    [Fact]
    public void Unblock_ActiveCredential_Throws()
    {
        Credential credential = TestData.ActiveCredential();

        Assert.Throws<BusinessRuleException>(() => credential.Unblock());
    }

    [Fact]
    public void Revoke_IsFinal()
    {
        Credential credential = TestData.ActiveCredential();
        credential.Revoke("Terminación del contrato");

        Assert.Equal(CredentialStatus.Revoked, credential.Status);
        Assert.Throws<BusinessRuleException>(() => credential.Revoke("Terminación del contrato"));
        Assert.Throws<BusinessRuleException>(() => credential.Block("Reporte de pérdida"));
        Assert.Throws<BusinessRuleException>(() => credential.Unblock());
    }

    [Theory]
    [InlineData("")]
    [InlineData("x")]
    public void Block_WithShortReason_Throws(string reason)
    {
        Credential credential = TestData.ActiveCredential();

        Assert.Throws<BusinessRuleException>(() => credential.Block(reason));
    }

    [Fact]
    public void Generate_CreatesCodeWithExpectedFormat()
    {
        CredentialCode code = CredentialCode.Generate();

        Assert.Matches(@"^CRD-[A-Z0-9]{8}$", code.Value);
    }

    [Fact]
    public void Constructor_NormalizesCodeToUppercase()
    {
        CredentialCode code = new(" crd-abcd2345 ");

        Assert.Equal("CRD-ABCD2345", code.Value);
    }

    [Theory]
    [InlineData("ABC-12345678")]
    [InlineData("CRD-123")]
    [InlineData("")]
    public void Constructor_InvalidCode_Throws(string value)
    {
        Assert.Throws<BusinessRuleException>(() => new CredentialCode(value));
    }
}
