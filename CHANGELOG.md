# Cambios

## [1.0.0] - 2026-10-28

Primera versión del Sistema de Gestión y Control de Acceso de Empleados (Seguimiento 2).

### Requerimientos funcionales

- **RF-01 Gestión de empleados:** registrar, consultar (con búsqueda, estado y área), actualizar y desactivar empleados, con identificación y correo únicos.
- **RF-02 Gestión de credenciales:** asignar credenciales con código único generado por el sistema, consultar su estado, bloquearlas, desbloquearlas y revocarlas.
- **RF-03 Registro de entradas y salidas:** validar cada solicitud con el servicio de dominio `AccessPolicy` y registrar el resultado, incluidos los rechazos con su motivo.
- **RF-04 Consulta del historial de accesos:** filtros por empleado, rango de fechas, punto de acceso y resultado, con paginación y un repositorio de solo lectura.
- **RF-06 Administración de puntos de acceso:** registrar, consultar, habilitar e inhabilitar puntos de acceso, con cuatro puntos iniciales como datos semilla.

### Arquitectura

- Clean Architecture en cuatro proyectos (Domain, Application, Persistence y API) con DDD y CQRS.
- Mediador propio, Repository y Unit of Work sobre Entity Framework Core y SQL Server.
- Manejo global de errores con ProblemDetails (400, 404 y 409) y documentación OpenAPI con Scalar.

### Calidad y documentación

- 48 pruebas unitarias con xUnit del dominio y de los casos de uso.
- Evidencias con 46 peticiones reales, colección de Postman y archivos `.http` por requerimiento.
- README con los diagramas de arquitectura, del modelo de dominio y de la regla de acceso.
