namespace Sgcae.Domain.Entities.Employees;

/// <summary>
/// Estado laboral del empleado. Un empleado inactivo no puede ingresar a las instalaciones
/// ni recibir credenciales nuevas.
/// </summary>
public enum EmploymentStatus
{
    Active = 1,
    Inactive = 2
}
