using Sgcae.Application.Contracts.Repositories;
using Sgcae.Application.Utilities.Mediator;
using Sgcae.Application.Utilities.Pagination;
using Sgcae.Domain.Entities.Credentials;

namespace Sgcae.Application.UseCases.Credentials.Queries.GetCredentialList;

public sealed class GetCredentialListUseCase(ICredentialRepository credentialRepository)
    : IRequestHandler<GetCredentialListQuery, PaginationResponse<CredentialListItemDto>>
{
    public async Task<PaginationResponse<CredentialListItemDto>> Handle(
        GetCredentialListQuery request, CancellationToken cancellationToken)
    {
        PaginationResponse<Credential> page = await credentialRepository.GetPagedListAsync(
            request.Pagination, request.EmployeeId, request.Status, cancellationToken);

        List<CredentialListItemDto> items = page.Items.Select(credential => credential.ToListItemDto()).ToList();

        return PaginationResponse<CredentialListItemDto>.Create(items, page.TotalCount, request.Pagination);
    }
}
