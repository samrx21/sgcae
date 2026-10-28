using Sgcae.Application.Utilities.Mediator;

namespace Sgcae.Application.UseCases.Credentials.Queries.GetCredentialByCode;

/// <summary>
/// Query para consultar una credencial y su estado a partir de su código.
/// </summary>
public sealed record GetCredentialByCodeQuery(string Code) : IRequest<CredentialDetailDto>;
