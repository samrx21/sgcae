using Microsoft.EntityFrameworkCore;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Credentials;
using Sgcae.Domain.Entities.Credentials.ValueObjects;
using Sgcae.Persistence.Extensions;

namespace Sgcae.Persistence.Repositories;

public sealed class CredentialRepository(SgcaeDbContext context) : Repository<Credential>(context), ICredentialRepository
{
    public async Task<Credential?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        // El código se busca como se guarda: en mayúsculas y sin espacios
        string value = CredentialCode.Normalize(code);
        return await Context.Set<Credential>().FirstOrDefaultAsync(credential => credential.Code.Value == value, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        string value = CredentialCode.Normalize(code);
        return await Context.Set<Credential>().AnyAsync(credential => credential.Code.Value == value, cancellationToken);
    }

    public async Task<bool> HasCurrentCredentialAsync(Guid employeeId, CancellationToken cancellationToken = default) =>
        await Context.Set<Credential>().AnyAsync(
            credential => credential.EmployeeId == employeeId && credential.Status != CredentialStatus.Revoked,
            cancellationToken);

    public async Task<PaginationResponse<Credential>> GetPagedListAsync(
        PaginationRequest pagination,
        Guid? employeeId,
        CredentialStatus? status,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Credential> query = Context.Set<Credential>().AsNoTracking();

        if (employeeId.HasValue)
            query = query.Where(credential => credential.EmployeeId == employeeId.Value);

        if (status.HasValue)
            query = query.Where(credential => credential.Status == status.Value);

        return await query
            .OrderByDescending(credential => credential.IssuedAt)
            .ThenBy(credential => credential.Id)
            .ToPagedListAsync(pagination, cancellationToken);
    }
}
