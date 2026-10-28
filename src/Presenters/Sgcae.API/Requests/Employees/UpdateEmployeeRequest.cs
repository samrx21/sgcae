namespace Sgcae.API.Requests.Employees;

/// <summary>
/// Cuerpo de la petición para actualizar un empleado. El Id viaja en la ruta.
/// </summary>
public sealed record UpdateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    string Area);
