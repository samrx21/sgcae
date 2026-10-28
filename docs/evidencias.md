# Evidencias de funcionamiento

Peticiones reales ejecutadas contra la API (`http://localhost:5190`) con la base de datos recién creada.
Se muestran en el orden en que se ejecutaron, porque cada paso depende del anterior.
La misma secuencia está en la colección de Postman (`docs/postman/SGCAE.postman_collection.json`).

**Resultado: 46 de 46 pasos con la respuesta esperada.**

## Resumen

| # | RF | Paso | Petición | Respuesta |
|---|---|---|---|---|
| 1 | RF-06 | Listar puntos de acceso (datos semilla) | `GET /api/access-points` | **200**  |
| 2 | RF-06 | Registrar un punto de acceso | `POST /api/access-points` | **201**  |
| 3 | RF-06 | Consultar un punto de acceso | `GET /api/access-points/01a182d5-94ff-7488-8984-71028b71a763` | **200**  |
| 4 | RF-06 | Listar solo los puntos habilitados | `GET /api/access-points?isEnabled=true` | **200**  |
| 5 | RF-01 | Registrar empleado (Laura) | `POST /api/employees` | **201**  |
| 6 | RF-01 | Registrar empleado (Andrés) | `POST /api/employees` | **201**  |
| 7 | RF-01 | Listar empleados con búsqueda | `GET /api/employees?search=gomez` | **200**  |
| 8 | RF-01 | Consultar empleado | `GET /api/employees/01a182d5-95b3-7f4b-b981-a449809b81ef` | **200**  |
| 9 | RF-01 | Actualizar empleado | `PUT /api/employees/01a182d5-95b3-7f4b-b981-a449809b81ef` | **204**  |
| 10 | RF-02 | Asignar credencial (Laura) | `POST /api/credentials` | **201**  |
| 11 | RF-02 | Consultar credencial por código | `GET /api/credentials/CRD-7GCHUTG8` | **200**  |
| 12 | RF-02 | Listar credenciales de un empleado | `GET /api/credentials?employeeId=01a182d5-95b3-7f4b-b981-a449809b81ef` | **200**  |
| 13 | RF-03 | Entrada autorizada por recepción | `POST /api/access-requests` | **201** Entrada autorizada. |
| 14 | RF-03 | Rechazo: segunda entrada sin haber salido | `POST /api/access-requests` | **201** Acceso rechazado. El empleado ya registró una entrada y no ha registrado su salida. |
| 15 | RF-03 | Salida autorizada | `POST /api/access-requests` | **201** Salida autorizada. |
| 16 | RF-03 | Rechazo: salida sin entrada previa | `POST /api/access-requests` | **201** Acceso rechazado. El empleado no tiene una entrada registrada para poder salir. |
| 17 | RF-03 | Rechazo: credencial inexistente | `POST /api/access-requests` | **201** Acceso rechazado. La credencial no está registrada. |
| 18 | RF-02 | Bloquear credencial | `PATCH /api/credentials/CRD-7GCHUTG8/block` | **204**  |
| 19 | RF-03 | Rechazo: credencial bloqueada | `POST /api/access-requests` | **201** Acceso rechazado. La credencial está bloqueada. |
| 20 | RF-02 | Desbloquear credencial | `PATCH /api/credentials/CRD-7GCHUTG8/unblock` | **204**  |
| 21 | RF-06 | Inhabilitar punto de acceso (bodega) | `PATCH /api/access-points/0199f7a0-0000-7000-8000-000000000003/disable` | **204**  |
| 22 | RF-03 | Rechazo: punto de acceso inhabilitado | `POST /api/access-requests` | **201** Acceso rechazado. El punto de acceso está inhabilitado. |
| 23 | RF-06 | Habilitar punto de acceso (bodega) | `PATCH /api/access-points/0199f7a0-0000-7000-8000-000000000003/enable` | **204**  |
| 24 | RF-03 | Entrada autorizada por bodega | `POST /api/access-requests` | **201** Entrada autorizada. |
| 25 | RF-02 | Asignar credencial (Andrés) | `POST /api/credentials` | **201**  |
| 26 | RF-01 | Desactivar empleado (Andrés) | `PATCH /api/employees/01a182d5-95dd-7a32-be94-1ef1c5854943/deactivate` | **204**  |
| 27 | RF-03 | Rechazo: empleado inactivo | `POST /api/access-requests` | **201** Acceso rechazado. El empleado no tiene un vínculo laboral activo. |
| 28 | RF-02 | Revocar credencial (Andrés) | `PATCH /api/credentials/CRD-STAKCN55/revoke` | **204**  |
| 29 | RF-03 | Rechazo: credencial revocada | `POST /api/access-requests` | **201** Acceso rechazado. La credencial fue revocada. |
| 30 | RF-04 | Historial completo (página de 5) | `GET /api/access-history?pageSize=5` | **200**  |
| 31 | RF-04 | Historial de un empleado, solo rechazos | `GET /api/access-history?employeeId=01a182d5-95b3-7f4b-b981-a449809b81ef&result=Denied` | **200**  |
| 32 | RF-04 | Historial de un punto de acceso en un rango de fechas | `GET /api/access-history?accessPointId=0199f7a0-0000-7000-8000-000000000001&from=2026-10-28&to=2026-10-28` | **200**  |
| 33 | RF-01 | 400 · Correo inválido | `POST /api/employees` | **400** El correo electrónico no tiene un formato válido. |
| 34 | RF-01 | 400 · Identificación con letras | `POST /api/employees` | **400** El número de identificación solo puede contener números. |
| 35 | RF-01 | 409 · Identificación duplicada | `POST /api/employees` | **409** Ya existe un empleado registrado con la identificación 1037654321. |
| 36 | RF-01 | 400 · Actualizar un empleado inactivo | `PUT /api/employees/01a182d5-95dd-7a32-be94-1ef1c5854943` | **400** No se pueden modificar los datos de un empleado inactivo. |
| 37 | RF-01 | 404 · Empleado inexistente | `GET /api/employees/00000000-0000-0000-0000-000000000001` | **404** No existe un empleado con el Id 00000000-0000-0000-0000-000000000001. |
| 38 | RF-02 | 409 · El empleado ya tiene una credencial vigente | `POST /api/credentials` | **409** El empleado ya tiene una credencial activa o bloqueada. Revóquela antes de asignar una nueva. |
| 39 | RF-02 | 400 · Asignar credencial a un empleado inactivo | `POST /api/credentials` | **400** No se puede asignar una credencial a un empleado inactivo. |
| 40 | RF-02 | 400 · Revocar una credencial ya revocada | `PATCH /api/credentials/CRD-STAKCN55/revoke` | **400** La credencial ya se encuentra revocada. |
| 41 | RF-02 | 400 · Motivo de bloqueo demasiado corto | `PATCH /api/credentials/CRD-7GCHUTG8/block` | **400** El motivo debe tener entre 5 y 200 caracteres. |
| 42 | RF-03 | 404 · Punto de acceso inexistente | `POST /api/access-requests` | **404** No existe un punto de acceso con el Id 00000000-0000-0000-0000-000000000009. |
| 43 | RF-04 | 400 · Rango de fechas invertido | `GET /api/access-history?from=2026-12-31&to=2026-01-01` | **400** La fecha inicial no puede ser posterior a la fecha final. |
| 44 | RF-06 | 409 · Nombre de punto de acceso duplicado | `POST /api/access-points` | **409** Ya existe un punto de acceso con el nombre Parqueadero. |
| 45 | RF-06 | Inhabilitar un punto de acceso | `PATCH /api/access-points/01a182d5-94ff-7488-8984-71028b71a763/disable` | **204**  |
| 46 | RF-06 | 400 · Inhabilitar un punto ya inhabilitado | `PATCH /api/access-points/01a182d5-94ff-7488-8984-71028b71a763/disable` | **400** El punto de acceso ya se encuentra inhabilitado. |

## Detalle

### 1. RF-06 · Listar puntos de acceso (datos semilla)

```http
GET /api/access-points
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "0199f7a0-0000-7000-8000-000000000003",
      "name": "Bodega central",
      "type": "Warehouse",
      "location": "Zona de carga y descarga",
      "isEnabled": true,
      "createdAt": "2026-10-27T00:00:00Z",
      "updatedAt": null
    },
    {
      "id": "0199f7a0-0000-7000-8000-000000000002",
      "name": "Parqueadero",
      "type": "Parking",
      "location": "Sótano, acceso vehicular",
      "isEnabled": true,
      "createdAt": "2026-10-27T00:00:00Z",
      "updatedAt": null
    },
    "... 2 elementos más"
  ],
  "totalCount": 4,
  "pageNumber": 1,
  "pageSize": 15,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 2. RF-06 · Registrar un punto de acceso

```http
POST /api/access-points
Content-Type: application/json

{
  "name": "Archivo documental",
  "type": "RestrictedArea",
  "location": "Tercer piso, oficina 302"
}
```

Respuesta **201**:

```json
{
  "id": "01a182d5-94ff-7488-8984-71028b71a763"
}
```

### 3. RF-06 · Consultar un punto de acceso

```http
GET /api/access-points/01a182d5-94ff-7488-8984-71028b71a763
```

Respuesta **200**:

```json
{
  "id": "01a182d5-94ff-7488-8984-71028b71a763",
  "name": "Archivo documental",
  "type": "RestrictedArea",
  "location": "Tercer piso, oficina 302",
  "isEnabled": true,
  "createdAt": "2026-10-28T14:06:37.823229Z",
  "updatedAt": null
}
```

### 4. RF-06 · Listar solo los puntos habilitados

```http
GET /api/access-points?isEnabled=true
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a182d5-94ff-7488-8984-71028b71a763",
      "name": "Archivo documental",
      "type": "RestrictedArea",
      "location": "Tercer piso, oficina 302",
      "isEnabled": true,
      "createdAt": "2026-10-28T14:06:37.823229Z",
      "updatedAt": null
    },
    {
      "id": "0199f7a0-0000-7000-8000-000000000003",
      "name": "Bodega central",
      "type": "Warehouse",
      "location": "Zona de carga y descarga",
      "isEnabled": true,
      "createdAt": "2026-10-27T00:00:00Z",
      "updatedAt": null
    },
    "... 3 elementos más"
  ],
  "totalCount": 5,
  "pageNumber": 1,
  "pageSize": 15,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 5. RF-01 · Registrar empleado (Laura)

```http
POST /api/employees
Content-Type: application/json

{
  "identificationNumber": "1037654321",
  "firstName": "Laura",
  "lastName": "Gómez",
  "email": "laura.gomez@logistica.com",
  "jobTitle": "Analista de inventarios",
  "area": "Bodega"
}
```

Respuesta **201**:

```json
{
  "id": "01a182d5-95b3-7f4b-b981-a449809b81ef"
}
```

### 6. RF-01 · Registrar empleado (Andrés)

```http
POST /api/employees
Content-Type: application/json

{
  "identificationNumber": "1020304050",
  "firstName": "Andrés",
  "lastName": "Restrepo",
  "email": "andres.restrepo@logistica.com",
  "jobTitle": "Conductor",
  "area": "Transporte"
}
```

Respuesta **201**:

```json
{
  "id": "01a182d5-95dd-7a32-be94-1ef1c5854943"
}
```

### 7. RF-01 · Listar empleados con búsqueda

```http
GET /api/employees?search=gomez
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a182d5-95b3-7f4b-b981-a449809b81ef",
      "identificationNumber": "1037654321",
      "fullName": "Laura Gómez",
      "email": "laura.gomez@logistica.com",
      "jobTitle": "Analista de inventarios",
      "area": "Bodega",
      "status": "Active"
    }
  ],
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 15,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 8. RF-01 · Consultar empleado

```http
GET /api/employees/01a182d5-95b3-7f4b-b981-a449809b81ef
```

Respuesta **200**:

```json
{
  "id": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "identificationNumber": "1037654321",
  "firstName": "Laura",
  "lastName": "Gómez",
  "fullName": "Laura Gómez",
  "email": "laura.gomez@logistica.com",
  "jobTitle": "Analista de inventarios",
  "area": "Bodega",
  "status": "Active",
  "createdAt": "2026-10-28T14:06:38.003371Z",
  "updatedAt": null
}
```

### 9. RF-01 · Actualizar empleado

```http
PUT /api/employees/01a182d5-95b3-7f4b-b981-a449809b81ef
Content-Type: application/json

{
  "firstName": "Laura",
  "lastName": "Gómez",
  "email": "laura.gomez@logistica.com",
  "jobTitle": "Coordinadora de inventarios",
  "area": "Bodega"
}
```

Respuesta **204** (sin contenido).

### 10. RF-02 · Asignar credencial (Laura)

```http
POST /api/credentials
Content-Type: application/json

{
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef"
}
```

Respuesta **201**:

```json
{
  "id": "01a182d5-964d-70b4-ab87-309a8e4d2fa6",
  "code": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "status": "Active",
  "canBeUsed": true,
  "issuedAt": "2026-10-28T14:06:38.1576204Z",
  "statusChangedAt": null,
  "statusReason": null
}
```

### 11. RF-02 · Consultar credencial por código

```http
GET /api/credentials/CRD-7GCHUTG8
```

Respuesta **200**:

```json
{
  "id": "01a182d5-964d-70b4-ab87-309a8e4d2fa6",
  "code": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "status": "Active",
  "canBeUsed": true,
  "issuedAt": "2026-10-28T14:06:38.1576204Z",
  "statusChangedAt": null,
  "statusReason": null
}
```

### 12. RF-02 · Listar credenciales de un empleado

```http
GET /api/credentials?employeeId=01a182d5-95b3-7f4b-b981-a449809b81ef
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a182d5-964d-70b4-ab87-309a8e4d2fa6",
      "code": "CRD-7GCHUTG8",
      "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
      "status": "Active",
      "canBeUsed": true,
      "issuedAt": "2026-10-28T14:06:38.1576204Z"
    }
  ],
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 15,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 13. RF-03 · Entrada autorizada por recepción

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-96ac-7ee1-a2cd-024fa7a0c97a",
  "result": "Granted",
  "rejectionReason": null,
  "message": "Entrada autorizada.",
  "movementType": "Entry",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-28T14:06:38.2527677Z"
}
```

### 14. RF-03 · Rechazo: segunda entrada sin haber salido

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-96dc-72b5-a2d9-384c23feecd2",
  "result": "Denied",
  "rejectionReason": "AlreadyInside",
  "message": "Acceso rechazado. El empleado ya registró una entrada y no ha registrado su salida.",
  "movementType": "Entry",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "accessPointName": "Parqueadero",
  "occurredAt": "2026-10-28T14:06:38.3005634Z"
}
```

### 15. RF-03 · Salida autorizada

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Exit"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-96e6-761a-a593-eab259600436",
  "result": "Granted",
  "rejectionReason": null,
  "message": "Salida autorizada.",
  "movementType": "Exit",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-28T14:06:38.3104467Z"
}
```

### 16. RF-03 · Rechazo: salida sin entrada previa

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Exit"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-96eb-7fd5-88aa-6cb243408dee",
  "result": "Denied",
  "rejectionReason": "NotInside",
  "message": "Acceso rechazado. El empleado no tiene una entrada registrada para poder salir.",
  "movementType": "Exit",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-28T14:06:38.3158546Z"
}
```

### 17. RF-03 · Rechazo: credencial inexistente

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-NOEXISTE",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-96f0-7ec9-abaf-e58cb2b8400e",
  "result": "Denied",
  "rejectionReason": "CredentialNotFound",
  "message": "Acceso rechazado. La credencial no está registrada.",
  "movementType": "Entry",
  "credentialCode": "CRD-NOEXISTE",
  "employeeId": null,
  "employeeFullName": null,
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-28T14:06:38.3203159Z"
}
```

### 18. RF-02 · Bloquear credencial

```http
PATCH /api/credentials/CRD-7GCHUTG8/block
Content-Type: application/json

{
  "reason": "Reporte de pérdida del carné"
}
```

Respuesta **204** (sin contenido).

### 19. RF-03 · Rechazo: credencial bloqueada

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-9703-7133-9756-a7cb1480d350",
  "result": "Denied",
  "rejectionReason": "CredentialBlocked",
  "message": "Acceso rechazado. La credencial está bloqueada.",
  "movementType": "Entry",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-28T14:06:38.339606Z"
}
```

### 20. RF-02 · Desbloquear credencial

```http
PATCH /api/credentials/CRD-7GCHUTG8/unblock
```

Respuesta **204** (sin contenido).

### 21. RF-06 · Inhabilitar punto de acceso (bodega)

```http
PATCH /api/access-points/0199f7a0-0000-7000-8000-000000000003/disable
```

Respuesta **204** (sin contenido).

### 22. RF-03 · Rechazo: punto de acceso inhabilitado

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-9728-7ff0-8320-880ff8d95f2b",
  "result": "Denied",
  "rejectionReason": "AccessPointDisabled",
  "message": "Acceso rechazado. El punto de acceso está inhabilitado.",
  "movementType": "Entry",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "accessPointName": "Bodega central",
  "occurredAt": "2026-10-28T14:06:38.3769173Z"
}
```

### 23. RF-06 · Habilitar punto de acceso (bodega)

```http
PATCH /api/access-points/0199f7a0-0000-7000-8000-000000000003/enable
```

Respuesta **204** (sin contenido).

### 24. RF-03 · Entrada autorizada por bodega

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-9736-74d1-a95e-67e250cb86a4",
  "result": "Granted",
  "rejectionReason": null,
  "message": "Entrada autorizada.",
  "movementType": "Entry",
  "credentialCode": "CRD-7GCHUTG8",
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "accessPointName": "Bodega central",
  "occurredAt": "2026-10-28T14:06:38.3905831Z"
}
```

### 25. RF-02 · Asignar credencial (Andrés)

```http
POST /api/credentials
Content-Type: application/json

{
  "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943"
}
```

Respuesta **201**:

```json
{
  "id": "01a182d5-973d-73a0-9d6d-aaba4d135f0b",
  "code": "CRD-STAKCN55",
  "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943",
  "employeeFullName": "Andrés Restrepo",
  "status": "Active",
  "canBeUsed": true,
  "issuedAt": "2026-10-28T14:06:38.3970375Z",
  "statusChangedAt": null,
  "statusReason": null
}
```

### 26. RF-01 · Desactivar empleado (Andrés)

```http
PATCH /api/employees/01a182d5-95dd-7a32-be94-1ef1c5854943/deactivate
```

Respuesta **204** (sin contenido).

### 27. RF-03 · Rechazo: empleado inactivo

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-STAKCN55",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-974a-7a4b-8033-5b58fb405bd8",
  "result": "Denied",
  "rejectionReason": "EmployeeInactive",
  "message": "Acceso rechazado. El empleado no tiene un vínculo laboral activo.",
  "movementType": "Entry",
  "credentialCode": "CRD-STAKCN55",
  "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943",
  "employeeFullName": "Andrés Restrepo",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "accessPointName": "Parqueadero",
  "occurredAt": "2026-10-28T14:06:38.4106029Z"
}
```

### 28. RF-02 · Revocar credencial (Andrés)

```http
PATCH /api/credentials/CRD-STAKCN55/revoke
Content-Type: application/json

{
  "reason": "Terminación del contrato laboral"
}
```

Respuesta **204** (sin contenido).

### 29. RF-03 · Rechazo: credencial revocada

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-STAKCN55",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a182d5-9758-76b2-b21d-c9a9d6f2be2a",
  "result": "Denied",
  "rejectionReason": "CredentialRevoked",
  "message": "Acceso rechazado. La credencial fue revocada.",
  "movementType": "Entry",
  "credentialCode": "CRD-STAKCN55",
  "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943",
  "employeeFullName": "Andrés Restrepo",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "accessPointName": "Parqueadero",
  "occurredAt": "2026-10-28T14:06:38.4241847Z"
}
```

### 30. RF-04 · Historial completo (página de 5)

```http
GET /api/access-history?pageSize=5
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a182d5-9758-76b2-b21d-c9a9d6f2be2a",
      "occurredAt": "2026-10-28T14:06:38.4241847Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialRevoked",
      "credentialCode": "CRD-STAKCN55",
      "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943",
      "employeeIdentification": "1020304050",
      "employeeFullName": "Andrés Restrepo",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
      "accessPointName": "Parqueadero",
      "rejectionDescription": "La credencial fue revocada."
    },
    {
      "id": "01a182d5-974a-7a4b-8033-5b58fb405bd8",
      "occurredAt": "2026-10-28T14:06:38.4106029Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "EmployeeInactive",
      "credentialCode": "CRD-STAKCN55",
      "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943",
      "employeeIdentification": "1020304050",
      "employeeFullName": "Andrés Restrepo",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
      "accessPointName": "Parqueadero",
      "rejectionDescription": "El empleado no tiene un vínculo laboral activo."
    },
    "... 3 elementos más"
  ],
  "totalCount": 10,
  "pageNumber": 1,
  "pageSize": 5,
  "totalPages": 2,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

### 31. RF-04 · Historial de un empleado, solo rechazos

```http
GET /api/access-history?employeeId=01a182d5-95b3-7f4b-b981-a449809b81ef&result=Denied
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a182d5-9728-7ff0-8320-880ff8d95f2b",
      "occurredAt": "2026-10-28T14:06:38.3769173Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "AccessPointDisabled",
      "credentialCode": "CRD-7GCHUTG8",
      "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
      "employeeIdentification": "1037654321",
      "employeeFullName": "Laura Gómez",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
      "accessPointName": "Bodega central",
      "rejectionDescription": "El punto de acceso está inhabilitado."
    },
    {
      "id": "01a182d5-9703-7133-9756-a7cb1480d350",
      "occurredAt": "2026-10-28T14:06:38.339606Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialBlocked",
      "credentialCode": "CRD-7GCHUTG8",
      "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
      "employeeIdentification": "1037654321",
      "employeeFullName": "Laura Gómez",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
      "accessPointName": "Recepción principal",
      "rejectionDescription": "La credencial está bloqueada."
    },
    "... 2 elementos más"
  ],
  "totalCount": 4,
  "pageNumber": 1,
  "pageSize": 15,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 32. RF-04 · Historial de un punto de acceso en un rango de fechas

```http
GET /api/access-history?accessPointId=0199f7a0-0000-7000-8000-000000000001&from=2026-10-28&to=2026-10-28
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a182d5-9703-7133-9756-a7cb1480d350",
      "occurredAt": "2026-10-28T14:06:38.339606Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialBlocked",
      "credentialCode": "CRD-7GCHUTG8",
      "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef",
      "employeeIdentification": "1037654321",
      "employeeFullName": "Laura Gómez",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
      "accessPointName": "Recepción principal",
      "rejectionDescription": "La credencial está bloqueada."
    },
    {
      "id": "01a182d5-96f0-7ec9-abaf-e58cb2b8400e",
      "occurredAt": "2026-10-28T14:06:38.3203159Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialNotFound",
      "credentialCode": "CRD-NOEXISTE",
      "employeeId": null,
      "employeeIdentification": null,
      "employeeFullName": null,
      "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
      "accessPointName": "Recepción principal",
      "rejectionDescription": "La credencial no está registrada."
    },
    "... 3 elementos más"
  ],
  "totalCount": 5,
  "pageNumber": 1,
  "pageSize": 15,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

### 33. RF-01 · 400 · Correo inválido

```http
POST /api/employees
Content-Type: application/json

{
  "identificationNumber": "99887766",
  "firstName": "Andrés",
  "lastName": "Restrepo",
  "email": "correo-sin-arroba",
  "jobTitle": "Conductor",
  "area": "Transporte"
}
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "El correo electrónico no tiene un formato válido."
}
```

### 34. RF-01 · 400 · Identificación con letras

```http
POST /api/employees
Content-Type: application/json

{
  "identificationNumber": "10AB2030",
  "firstName": "Andrés",
  "lastName": "Restrepo",
  "email": "otro@logistica.com",
  "jobTitle": "Conductor",
  "area": "Transporte"
}
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "El número de identificación solo puede contener números."
}
```

### 35. RF-01 · 409 · Identificación duplicada

```http
POST /api/employees
Content-Type: application/json

{
  "identificationNumber": "1037654321",
  "firstName": "Laura",
  "lastName": "Gómez",
  "email": "otra@logistica.com",
  "jobTitle": "Analista de inventarios",
  "area": "Bodega"
}
```

Respuesta **409**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.10",
  "title": "Conflicto con datos existentes",
  "status": 409,
  "detail": "Ya existe un empleado registrado con la identificación 1037654321."
}
```

### 36. RF-01 · 400 · Actualizar un empleado inactivo

```http
PUT /api/employees/01a182d5-95dd-7a32-be94-1ef1c5854943
Content-Type: application/json

{
  "firstName": "Andrés",
  "lastName": "Restrepo",
  "email": "andres.restrepo@logistica.com",
  "jobTitle": "Conductor",
  "area": "Transporte"
}
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "No se pueden modificar los datos de un empleado inactivo."
}
```

### 37. RF-01 · 404 · Empleado inexistente

```http
GET /api/employees/00000000-0000-0000-0000-000000000001
```

Respuesta **404**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Recurso no encontrado",
  "status": 404,
  "detail": "No existe un empleado con el Id 00000000-0000-0000-0000-000000000001."
}
```

### 38. RF-02 · 409 · El empleado ya tiene una credencial vigente

```http
POST /api/credentials
Content-Type: application/json

{
  "employeeId": "01a182d5-95b3-7f4b-b981-a449809b81ef"
}
```

Respuesta **409**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.10",
  "title": "Conflicto con datos existentes",
  "status": 409,
  "detail": "El empleado ya tiene una credencial activa o bloqueada. Revóquela antes de asignar una nueva."
}
```

### 39. RF-02 · 400 · Asignar credencial a un empleado inactivo

```http
POST /api/credentials
Content-Type: application/json

{
  "employeeId": "01a182d5-95dd-7a32-be94-1ef1c5854943"
}
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "No se puede asignar una credencial a un empleado inactivo."
}
```

### 40. RF-02 · 400 · Revocar una credencial ya revocada

```http
PATCH /api/credentials/CRD-STAKCN55/revoke
Content-Type: application/json

{
  "reason": "Intento duplicado de revocación"
}
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "La credencial ya se encuentra revocada."
}
```

### 41. RF-02 · 400 · Motivo de bloqueo demasiado corto

```http
PATCH /api/credentials/CRD-7GCHUTG8/block
Content-Type: application/json

{
  "reason": "x"
}
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "El motivo debe tener entre 5 y 200 caracteres."
}
```

### 42. RF-03 · 404 · Punto de acceso inexistente

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-7GCHUTG8",
  "accessPointId": "00000000-0000-0000-0000-000000000009",
  "movementType": "Entry"
}
```

Respuesta **404**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Recurso no encontrado",
  "status": 404,
  "detail": "No existe un punto de acceso con el Id 00000000-0000-0000-0000-000000000009."
}
```

### 43. RF-04 · 400 · Rango de fechas invertido

```http
GET /api/access-history?from=2026-12-31&to=2026-01-01
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Datos de la petición no válidos",
  "status": 400,
  "detail": "La fecha inicial no puede ser posterior a la fecha final."
}
```

### 44. RF-06 · 409 · Nombre de punto de acceso duplicado

```http
POST /api/access-points
Content-Type: application/json

{
  "name": "Parqueadero",
  "type": "Parking",
  "location": null
}
```

Respuesta **409**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.10",
  "title": "Conflicto con datos existentes",
  "status": 409,
  "detail": "Ya existe un punto de acceso con el nombre Parqueadero."
}
```

### 45. RF-06 · Inhabilitar un punto de acceso

```http
PATCH /api/access-points/01a182d5-94ff-7488-8984-71028b71a763/disable
```

Respuesta **204** (sin contenido).

### 46. RF-06 · 400 · Inhabilitar un punto ya inhabilitado

```http
PATCH /api/access-points/01a182d5-94ff-7488-8984-71028b71a763/disable
```

Respuesta **400**:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Regla de negocio incumplida",
  "status": 400,
  "detail": "El punto de acceso ya se encuentra inhabilitado."
}
```
