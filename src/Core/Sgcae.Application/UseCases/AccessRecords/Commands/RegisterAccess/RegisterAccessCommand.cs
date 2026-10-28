using Sgcae.Application.Utilities.Mediator;
using Sgcae.Domain.Entities.AccessRecords;

namespace Sgcae.Application.UseCases.AccessRecords.Commands.RegisterAccess;

/// <summary>
/// Solicitud de acceso que llega desde un punto de acceso: el código leído de la credencial,
/// el punto donde se presentó y si es una entrada o una salida.
/// </summary>
public sealed record RegisterAccessCommand(
    string CredentialCode,
    Guid AccessPointId,
    MovementType MovementType) : IRequest<AccessRegistrationDto>;
