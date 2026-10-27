namespace Sgcae.Application.Exceptions;

/// <summary>
/// Se lanza cuando los datos de una petición no son válidos para el caso de uso,
/// por ejemplo un rango de fechas invertido en una consulta.
/// La capa de presentación la traduce a una respuesta 400 (Bad Request).
/// </summary>
public sealed class ValidationException(string message) : Exception(message);
