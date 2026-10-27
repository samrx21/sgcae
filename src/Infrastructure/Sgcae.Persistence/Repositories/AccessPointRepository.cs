using Microsoft.EntityFrameworkCore;
using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.AccessPoints;
using Sgcae.Persistence.Extensions;

namespace Sgcae.Persistence.Repositories;

public sealed class AccessPointRepository(SgcaeDbContext context) : Repository<AccessPoint>(context), IAccessPointRepository
{
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        string value = name.Trim();
        return await Context.Set<AccessPoint>().AnyAsync(accessPoint => accessPoint.Name == value, cancellationToken);
    }

    public async Task<PaginationResponse<AccessPoint>> GetPagedListAsync(
        PaginationRequest pagination,
        AccessPointType? type,
        bool? isEnabled,
        CancellationToken cancellationToken = default)
    {
        IQueryable<AccessPoint> query = Context.Set<AccessPoint>().AsNoTracking();

        if (type.HasValue)
            query = query.Where(accessPoint => accessPoint.Type == type.Value);

        if (isEnabled.HasValue)
            query = query.Where(accessPoint => accessPoint.IsEnabled == isEnabled.Value);

        return await query
            .OrderBy(accessPoint => accessPoint.Name)
            .ThenBy(accessPoint => accessPoint.Id)
            .ToPagedListAsync(pagination, cancellationToken);
    }
}
