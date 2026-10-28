using Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;
using Sgcae.Application.UseCases.Credentials.Queries.GetCredentialList;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Employees;

namespace Sgcae.Application.UseCases.Credentials;

/// <summary>
/// Transformaciones de la entidad Credential hacia los DTOs que exponen los casos de uso.
/// </summary>
internal static class CredentialMapperExtensions
{
    public static CredentialDetailDto ToDetailDto(this Credential credential, Employee employee) =>
        new()
        {
            Id = credential.Id,
            Code = credential.Code.Value,
            EmployeeId = credential.EmployeeId,
            EmployeeFullName = employee.FullName,
            Status = credential.Status,
            CanBeUsed = credential.CanBeUsed,
            IssuedAt = credential.IssuedAt,
            StatusChangedAt = credential.StatusChangedAt,
            StatusReason = credential.StatusReason
        };

    public static CredentialListItemDto ToListItemDto(this Credential credential) =>
        new()
        {
            Id = credential.Id,
            Code = credential.Code.Value,
            EmployeeId = credential.EmployeeId,
            Status = credential.Status,
            CanBeUsed = credential.CanBeUsed,
            IssuedAt = credential.IssuedAt
        };
}
