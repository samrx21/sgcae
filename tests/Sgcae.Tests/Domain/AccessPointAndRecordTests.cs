using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Domain.Entities.AccessRecords;
using Sgcae.Domain.Exceptions;
using Sgcae.Domain.Services;

namespace Sgcae.Tests.Domain;

/// <summary>
/// Pruebas del RF-06 (puntos de acceso) y del registro de accesos del RF-03.
/// </summary>
public class AccessPointAndRecordTests
{
    [Fact]
    public void AccessPoint_IsCreatedEnabled()
    {
        AccessPoint accessPoint = TestData.EnabledAccessPoint();

        Assert.True(accessPoint.IsEnabled);
    }

    [Fact]
    public void AccessPoint_DisableTwice_Throws()
    {
        AccessPoint accessPoint = TestData.DisabledAccessPoint();

        Assert.Throws<BusinessRuleException>(() => accessPoint.Disable());
    }

    [Fact]
    public void AccessPoint_EnableWhenEnabled_Throws()
    {
        AccessPoint accessPoint = TestData.EnabledAccessPoint();

        Assert.Throws<BusinessRuleException>(() => accessPoint.Enable());
    }

    [Theory]
    [InlineData("")]
    [InlineData("AB")]
    public void AccessPoint_InvalidName_Throws(string name)
    {
        Assert.Throws<BusinessRuleException>(() => new AccessPoint(name, AccessPointType.Parking, null));
    }

    [Fact]
    public void AccessRecord_DeniedAttempt_KeepsReasonAndPresentedCode()
    {
        AccessRecord record = AccessRecord.Register(
            " crd-noexiste ", null, null, Guid.NewGuid(), MovementType.Entry,
            AccessDecision.Deny(RejectionReason.CredentialNotFound));

        Assert.Equal(AccessResult.Denied, record.Result);
        Assert.Equal(RejectionReason.CredentialNotFound, record.RejectionReason);
        Assert.Equal("CRD-NOEXISTE", record.CredentialCode);
    }

    [Fact]
    public void AccessRecord_GrantedWithoutEmployee_Throws()
    {
        Assert.Throws<BusinessRuleException>(() => AccessRecord.Register(
            "CRD-ABCD2345", Guid.NewGuid(), null, Guid.NewGuid(), MovementType.Entry, AccessDecision.Grant()));
    }

    [Fact]
    public void AccessRecord_CodeTooLong_Throws()
    {
        string code = new('A', AccessRecord.PRESENTED_CODE_MAX_LENGTH + 1);

        Assert.Throws<BusinessRuleException>(() => AccessRecord.Register(
            code, null, null, Guid.NewGuid(), MovementType.Entry, AccessDecision.Deny(RejectionReason.CredentialNotFound)));
    }

    [Fact]
    public void RejectionReason_HasDescriptionForEveryValue()
    {
        foreach (RejectionReason reason in Enum.GetValues<RejectionReason>())
            Assert.NotEqual("Acceso rechazado.", reason.ToDescription());
    }
}
