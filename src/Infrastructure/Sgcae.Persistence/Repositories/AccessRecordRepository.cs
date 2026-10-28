using Microsoft.EntityFrameworkCore;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Persistence.Repositories;

public sealed class AccessRecordRepository(SgcaeDbContext context) : Repository<AccessRecord>(context), IAccessRecordRepository
{
    public async Task<MovementType?> GetLastGrantedMovementAsync(Guid employeeId, CancellationToken cancellationToken = default) =>
        await Context.Set<AccessRecord>()
            .AsNoTracking()
            .Where(record => record.EmployeeId == employeeId && record.Result == AccessResult.Granted)
            .OrderByDescending(record => record.OccurredAt)
            .Select(record => (MovementType?)record.MovementType)
            .FirstOrDefaultAsync(cancellationToken);
}
