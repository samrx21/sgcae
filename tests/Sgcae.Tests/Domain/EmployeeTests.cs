using Sgcae.Domain.Common.ValueObjects;
using Sgcae.Domain.Entities.Employees;
using Sgcae.Domain.Entities.Employees.ValueObjects;
using Sgcae.Domain.Exceptions;

namespace Sgcae.Tests.Domain;

/// <summary>
/// Pruebas del RF-01: reglas del empleado y de sus value objects.
/// </summary>
public class EmployeeTests
{
    [Fact]
    public void Constructor_ValidData_CreatesActiveEmployee()
    {
        Employee employee = TestData.ActiveEmployee();

        Assert.NotEqual(Guid.Empty, employee.Id);
        Assert.Equal(EmploymentStatus.Active, employee.Status);
        Assert.Equal("Laura Gómez", employee.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    public void Constructor_InvalidFirstName_Throws(string firstName)
    {
        Assert.Throws<BusinessRuleException>(() => new Employee(
            new IdentificationNumber("1037654321"), firstName, "Gómez",
            new Email("laura@logistica.com"), "Analista", "Bodega"));
    }

    [Fact]
    public void UpdateInformation_ChangesData()
    {
        Employee employee = TestData.ActiveEmployee();

        employee.UpdateInformation("Laura", "Gómez", new Email("laura.nueva@logistica.com"), "Coordinadora", "Bodega");

        Assert.Equal("Coordinadora", employee.JobTitle);
        Assert.Equal("laura.nueva@logistica.com", employee.Email.Value);
        Assert.NotNull(employee.UpdatedAt);
    }

    [Fact]
    public void UpdateInformation_InactiveEmployee_Throws()
    {
        Employee employee = TestData.InactiveEmployee();

        Assert.Throws<BusinessRuleException>(() => employee.UpdateInformation(
            "Laura", "Gómez", new Email("laura@logistica.com"), "Analista", "Bodega"));
    }

    [Fact]
    public void Deactivate_Twice_Throws()
    {
        Employee employee = TestData.InactiveEmployee();

        Assert.Throws<BusinessRuleException>(() => employee.Deactivate());
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("10AB2030")]
    [InlineData("")]
    public void IdentificationNumber_Invalid_Throws(string value)
    {
        Assert.Throws<BusinessRuleException>(() => new IdentificationNumber(value));
    }

    [Theory]
    [InlineData("correo-sin-arroba")]
    [InlineData("")]
    public void Email_Invalid_Throws(string value)
    {
        Assert.Throws<BusinessRuleException>(() => new Email(value));
    }

    [Fact]
    public void Email_IsNormalizedToLowercase()
    {
        Assert.Equal(new Email("laura@logistica.com"), new Email(" Laura@Logistica.COM "));
    }
}
