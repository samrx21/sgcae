using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Application.UseCases.AccessRecords.Queries.GetAccessHistory;

/// <summary>
/// Filtros ya resueltos para el repositorio de lectura. Las fechas son instantes UTC:
/// desde <see cref="FromUtc"/> (incluido) hasta <see cref="ToUtcExclusive"/> (excluido).
/// </summary>
public sealed record AccessHistoryFilter(
    Guid? EmployeeId,
    DateTime? FromUtc,
    DateTime? ToUtcExclusive,
    Guid? AccessPointId,
    AccessResult? Result);
