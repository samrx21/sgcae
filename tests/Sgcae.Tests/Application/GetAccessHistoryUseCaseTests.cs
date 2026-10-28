using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Exceptions;
using Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;
using Sgcae.Application.Utilities.Pagination;

namespace Sgcae.Tests.Application;

/// <summary>
/// Pruebas del caso de uso del RF-04 con un repositorio de lectura falso.
/// </summary>
public class GetAccessHistoryUseCaseTests
{
    [Fact]
    public async Task Handle_InvertedDateRange_ThrowsValidationException()
    {
        FakeAccessHistoryReadRepository repository = new();
        GetAccessHistoryUseCase useCase = new(repository);
        GetAccessHistoryQuery query = new() { From = new DateOnly(2026, 12, 31), To = new DateOnly(2026, 1, 1) };

        await Assert.ThrowsAsync<ValidationException>(() => useCase.Handle(query, CancellationToken.None));
        Assert.Null(repository.ReceivedFilter);
    }

    [Fact]
    public async Task Handle_DateRange_IncludesTheWholeLastDay()
    {
        FakeAccessHistoryReadRepository repository = new();
        GetAccessHistoryUseCase useCase = new(repository);
        GetAccessHistoryQuery query = new() { From = new DateOnly(2026, 10, 1), To = new DateOnly(2026, 10, 31) };

        await useCase.Handle(query, CancellationToken.None);

        Assert.NotNull(repository.ReceivedFilter);
        Assert.Equal(new DateTime(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc), repository.ReceivedFilter.FromUtc);
        Assert.Equal(new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc), repository.ReceivedFilter.ToUtcExclusive);
    }

    private sealed class FakeAccessHistoryReadRepository : IAccessHistoryReadRepository
    {
        public AccessHistoryFilter? ReceivedFilter { get; private set; }

        public Task<PaginationResponse<AccessHistoryItemDto>> GetPagedAsync(
            AccessHistoryFilter filter, PaginationRequest pagination, CancellationToken cancellationToken = default)
        {
            ReceivedFilter = filter;
            return Task.FromResult(PaginationResponse<AccessHistoryItemDto>.Create([], 0, pagination));
        }
    }
}
