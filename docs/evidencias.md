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
| 3 | RF-06 | Consultar un punto de acceso | `GET /api/access-points/01a17e68-a1bf-7e85-8b5f-93813e252f0c` | **200**  |
| 4 | RF-06 | Listar solo los puntos habilitados | `GET /api/access-points?isEnabled=true` | **200**  |
| 5 | RF-01 | Registrar empleado (Laura) | `POST /api/employees` | **201**  |
| 6 | RF-01 | Registrar empleado (Andrés) | `POST /api/employees` | **201**  |
| 7 | RF-01 | Listar empleados con búsqueda | `GET /api/employees?search=gomez` | **200**  |
| 8 | RF-01 | Consultar empleado | `GET /api/employees/01a17e68-a282-76c1-b8e1-c7a05dfcf765` | **200**  |
| 9 | RF-01 | Actualizar empleado | `PUT /api/employees/01a17e68-a282-76c1-b8e1-c7a05dfcf765` | **204**  |
| 10 | RF-02 | Asignar credencial (Laura) | `POST /api/credentials` | **201**  |
| 11 | RF-02 | Consultar credencial por código | `GET /api/credentials/CRD-YBVVNDZX` | **200**  |
| 12 | RF-02 | Listar credenciales de un empleado | `GET /api/credentials?employeeId=01a17e68-a282-76c1-b8e1-c7a05dfcf765` | **200**  |
| 13 | RF-03 | Entrada autorizada por recepción | `POST /api/access-requests` | **201** Entrada autorizada. |
| 14 | RF-03 | Rechazo: segunda entrada sin haber salido | `POST /api/access-requests` | **201** Acceso rechazado. El empleado ya registró una entrada y no ha registrado su salida. |
| 15 | RF-03 | Salida autorizada | `POST /api/access-requests` | **201** Salida autorizada. |
| 16 | RF-03 | Rechazo: salida sin entrada previa | `POST /api/access-requests` | **201** Acceso rechazado. El empleado no tiene una entrada registrada para poder salir. |
| 17 | RF-03 | Rechazo: credencial inexistente | `POST /api/access-requests` | **201** Acceso rechazado. La credencial no está registrada. |
| 18 | RF-02 | Bloquear credencial | `PATCH /api/credentials/CRD-YBVVNDZX/block` | **204**  |
| 19 | RF-03 | Rechazo: credencial bloqueada | `POST /api/access-requests` | **201** Acceso rechazado. La credencial está bloqueada. |
| 20 | RF-02 | Desbloquear credencial | `PATCH /api/credentials/CRD-YBVVNDZX/unblock` | **204**  |
| 21 | RF-06 | Inhabilitar punto de acceso (bodega) | `PATCH /api/access-points/0199f7a0-0000-7000-8000-000000000003/disable` | **204**  |
| 22 | RF-03 | Rechazo: punto de acceso inhabilitado | `POST /api/access-requests` | **201** Acceso rechazado. El punto de acceso está inhabilitado. |
| 23 | RF-06 | Habilitar punto de acceso (bodega) | `PATCH /api/access-points/0199f7a0-0000-7000-8000-000000000003/enable` | **204**  |
| 24 | RF-03 | Entrada autorizada por bodega | `POST /api/access-requests` | **201** Entrada autorizada. |
| 25 | RF-02 | Asignar credencial (Andrés) | `POST /api/credentials` | **201**  |
| 26 | RF-01 | Desactivar empleado (Andrés) | `PATCH /api/employees/01a17e68-a2b9-7913-93d2-4e2333b741f0/deactivate` | **204**  |
| 27 | RF-03 | Rechazo: empleado inactivo | `POST /api/access-requests` | **201** Acceso rechazado. El empleado no tiene un vínculo laboral activo. |
| 28 | RF-02 | Revocar credencial (Andrés) | `PATCH /api/credentials/CRD-KCBJFUHX/revoke` | **204**  |
| 29 | RF-03 | Rechazo: credencial revocada | `POST /api/access-requests` | **201** Acceso rechazado. La credencial fue revocada. |
| 30 | RF-04 | Historial completo (página de 5) | `GET /api/access-history?pageSize=5` | **200**  |
| 31 | RF-04 | Historial de un empleado, solo rechazos | `GET /api/access-history?employeeId=01a17e68-a282-76c1-b8e1-c7a05dfcf765&result=Denied` | **200**  |
| 32 | RF-04 | Historial de un punto de acceso en un rango de fechas | `GET /api/access-history?accessPointId=0199f7a0-0000-7000-8000-000000000001&from=2026-10-27&to=2026-10-27` | **200**  |
| 33 | RF-01 | 400 · Correo inválido | `POST /api/employees` | **400** El correo electrónico no tiene un formato válido. |
| 34 | RF-01 | 400 · Identificación con letras | `POST /api/employees` | **400** El número de identificación solo puede contener números. |
| 35 | RF-01 | 409 · Identificación duplicada | `POST /api/employees` | **409** Ya existe un empleado registrado con la identificación 1037654321. |
| 36 | RF-01 | 400 · Actualizar un empleado inactivo | `PUT /api/employees/01a17e68-a2b9-7913-93d2-4e2333b741f0` | **400** No se pueden modificar los datos de un empleado inactivo. |
| 37 | RF-01 | 404 · Empleado inexistente | `GET /api/employees/00000000-0000-0000-0000-000000000001` | **404** No existe un empleado con el Id 00000000-0000-0000-0000-000000000001. |
| 38 | RF-02 | 409 · El empleado ya tiene una credencial vigente | `POST /api/credentials` | **409** El empleado ya tiene una credencial activa o bloqueada. Revóquela antes de asignar una nueva. |
| 39 | RF-02 | 400 · Asignar credencial a un empleado inactivo | `POST /api/credentials` | **400** No se puede asignar una credencial a un empleado inactivo. |
| 40 | RF-02 | 400 · Revocar una credencial ya revocada | `PATCH /api/credentials/CRD-KCBJFUHX/revoke` | **400** La credencial ya se encuentra revocada. |
| 41 | RF-02 | 400 · Motivo de bloqueo demasiado corto | `PATCH /api/credentials/CRD-YBVVNDZX/block` | **400** El motivo debe tener entre 5 y 200 caracteres. |
| 42 | RF-03 | 404 · Punto de acceso inexistente | `POST /api/access-requests` | **404** No existe un punto de acceso con el Id 00000000-0000-0000-0000-000000000009. |
| 43 | RF-04 | 400 · Rango de fechas invertido | `GET /api/access-history?from=2026-12-31&to=2026-01-01` | **400** La fecha inicial no puede ser posterior a la fecha final. |
| 44 | RF-06 | 409 · Nombre de punto de acceso duplicado | `POST /api/access-points` | **409** Ya existe un punto de acceso con el nombre Parqueadero. |
| 45 | RF-06 | Inhabilitar un punto de acceso | `PATCH /api/access-points/01a17e68-a1bf-7e85-8b5f-93813e252f0c/disable` | **204**  |
| 46 | RF-06 | 400 · Inhabilitar un punto ya inhabilitado | `PATCH /api/access-points/01a17e68-a1bf-7e85-8b5f-93813e252f0c/disable` | **400** El punto de acceso ya se encuentra inhabilitado. |

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
  "id": "01a17e68-a1bf-7e85-8b5f-93813e252f0c"
}
```

### 3. RF-06 · Consultar un punto de acceso

```http
GET /api/access-points/01a17e68-a1bf-7e85-8b5f-93813e252f0c
```

Respuesta **200**:

```json
{
  "id": "01a17e68-a1bf-7e85-8b5f-93813e252f0c",
  "name": "Archivo documental",
  "type": "RestrictedArea",
  "location": "Tercer piso, oficina 302",
  "isEnabled": true,
  "createdAt": "2026-10-27T17:29:08.799667Z",
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
      "id": "01a17e68-a1bf-7e85-8b5f-93813e252f0c",
      "name": "Archivo documental",
      "type": "RestrictedArea",
      "location": "Tercer piso, oficina 302",
      "isEnabled": true,
      "createdAt": "2026-10-27T17:29:08.799667Z",
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
  "id": "01a17e68-a282-76c1-b8e1-c7a05dfcf765"
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
  "id": "01a17e68-a2b9-7913-93d2-4e2333b741f0"
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
      "id": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
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
GET /api/employees/01a17e68-a282-76c1-b8e1-c7a05dfcf765
```

Respuesta **200**:

```json
{
  "id": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "identificationNumber": "1037654321",
  "firstName": "Laura",
  "lastName": "Gómez",
  "fullName": "Laura Gómez",
  "email": "laura.gomez@logistica.com",
  "jobTitle": "Analista de inventarios",
  "area": "Bodega",
  "status": "Active",
  "createdAt": "2026-10-27T17:29:08.9943966Z",
  "updatedAt": null
}
```

### 9. RF-01 · Actualizar empleado

```http
PUT /api/employees/01a17e68-a282-76c1-b8e1-c7a05dfcf765
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
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765"
}
```

Respuesta **201**:

```json
{
  "id": "01a17e68-a368-713e-905f-64f92b72a16b",
  "code": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "status": "Active",
  "canBeUsed": true,
  "issuedAt": "2026-10-27T17:29:09.2242817Z",
  "statusChangedAt": null,
  "statusReason": null
}
```

### 11. RF-02 · Consultar credencial por código

```http
GET /api/credentials/CRD-YBVVNDZX
```

Respuesta **200**:

```json
{
  "id": "01a17e68-a368-713e-905f-64f92b72a16b",
  "code": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "status": "Active",
  "canBeUsed": true,
  "issuedAt": "2026-10-27T17:29:09.2242817Z",
  "statusChangedAt": null,
  "statusReason": null
}
```

### 12. RF-02 · Listar credenciales de un empleado

```http
GET /api/credentials?employeeId=01a17e68-a282-76c1-b8e1-c7a05dfcf765
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a17e68-a368-713e-905f-64f92b72a16b",
      "code": "CRD-YBVVNDZX",
      "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
      "status": "Active",
      "canBeUsed": true,
      "issuedAt": "2026-10-27T17:29:09.2242817Z"
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
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a3e3-79d1-a60b-85743982480c",
  "result": "Granted",
  "rejectionReason": null,
  "message": "Entrada autorizada.",
  "movementType": "Entry",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-27T17:29:09.348277Z"
}
```

### 14. RF-03 · Rechazo: segunda entrada sin haber salido

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a41b-76f5-b374-ee357638330c",
  "result": "Denied",
  "rejectionReason": "AlreadyInside",
  "message": "Acceso rechazado. El empleado ya registró una entrada y no ha registrado su salida.",
  "movementType": "Entry",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "accessPointName": "Parqueadero",
  "occurredAt": "2026-10-27T17:29:09.4039522Z"
}
```

### 15. RF-03 · Salida autorizada

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Exit"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a428-7650-aeb7-dd634209ac5f",
  "result": "Granted",
  "rejectionReason": null,
  "message": "Salida autorizada.",
  "movementType": "Exit",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-27T17:29:09.4162324Z"
}
```

### 16. RF-03 · Rechazo: salida sin entrada previa

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Exit"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a42f-7c01-b376-b2efd72eb9f9",
  "result": "Denied",
  "rejectionReason": "NotInside",
  "message": "Acceso rechazado. El empleado no tiene una entrada registrada para poder salir.",
  "movementType": "Exit",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-27T17:29:09.4230367Z"
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
  "accessRecordId": "01a17e68-a434-720f-9af4-dd834e98c463",
  "result": "Denied",
  "rejectionReason": "CredentialNotFound",
  "message": "Acceso rechazado. La credencial no está registrada.",
  "movementType": "Entry",
  "credentialCode": "CRD-NOEXISTE",
  "employeeId": null,
  "employeeFullName": null,
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-27T17:29:09.4282262Z"
}
```

### 18. RF-02 · Bloquear credencial

```http
PATCH /api/credentials/CRD-YBVVNDZX/block
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
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a449-7bfc-b949-ce70b47a858b",
  "result": "Denied",
  "rejectionReason": "CredentialBlocked",
  "message": "Acceso rechazado. La credencial está bloqueada.",
  "movementType": "Entry",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
  "accessPointName": "Recepción principal",
  "occurredAt": "2026-10-27T17:29:09.4492206Z"
}
```

### 20. RF-02 · Desbloquear credencial

```http
PATCH /api/credentials/CRD-YBVVNDZX/unblock
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
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a472-7d0d-8a1a-645ee6b02a48",
  "result": "Denied",
  "rejectionReason": "AccessPointDisabled",
  "message": "Acceso rechazado. El punto de acceso está inhabilitado.",
  "movementType": "Entry",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "accessPointName": "Bodega central",
  "occurredAt": "2026-10-27T17:29:09.4905653Z"
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
  "credentialCode": "CRD-YBVVNDZX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a483-7937-a88e-babf0c364b75",
  "result": "Granted",
  "rejectionReason": null,
  "message": "Entrada autorizada.",
  "movementType": "Entry",
  "credentialCode": "CRD-YBVVNDZX",
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
  "employeeFullName": "Laura Gómez",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
  "accessPointName": "Bodega central",
  "occurredAt": "2026-10-27T17:29:09.5073445Z"
}
```

### 25. RF-02 · Asignar credencial (Andrés)

```http
POST /api/credentials
Content-Type: application/json

{
  "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0"
}
```

Respuesta **201**:

```json
{
  "id": "01a17e68-a48a-7762-a002-982b22a58d06",
  "code": "CRD-KCBJFUHX",
  "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0",
  "employeeFullName": "Andrés Restrepo",
  "status": "Active",
  "canBeUsed": true,
  "issuedAt": "2026-10-27T17:29:09.5145037Z",
  "statusChangedAt": null,
  "statusReason": null
}
```

### 26. RF-01 · Desactivar empleado (Andrés)

```http
PATCH /api/employees/01a17e68-a2b9-7913-93d2-4e2333b741f0/deactivate
```

Respuesta **204** (sin contenido).

### 27. RF-03 · Rechazo: empleado inactivo

```http
POST /api/access-requests
Content-Type: application/json

{
  "credentialCode": "CRD-KCBJFUHX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a49c-7b4b-8fec-95d19abcdb15",
  "result": "Denied",
  "rejectionReason": "EmployeeInactive",
  "message": "Acceso rechazado. El empleado no tiene un vínculo laboral activo.",
  "movementType": "Entry",
  "credentialCode": "CRD-KCBJFUHX",
  "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0",
  "employeeFullName": "Andrés Restrepo",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "accessPointName": "Parqueadero",
  "occurredAt": "2026-10-27T17:29:09.5323336Z"
}
```

### 28. RF-02 · Revocar credencial (Andrés)

```http
PATCH /api/credentials/CRD-KCBJFUHX/revoke
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
  "credentialCode": "CRD-KCBJFUHX",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "movementType": "Entry"
}
```

Respuesta **201**:

```json
{
  "accessRecordId": "01a17e68-a4aa-7616-8c7c-93067d241f0e",
  "result": "Denied",
  "rejectionReason": "CredentialRevoked",
  "message": "Acceso rechazado. La credencial fue revocada.",
  "movementType": "Entry",
  "credentialCode": "CRD-KCBJFUHX",
  "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0",
  "employeeFullName": "Andrés Restrepo",
  "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
  "accessPointName": "Parqueadero",
  "occurredAt": "2026-10-27T17:29:09.5465584Z"
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
      "id": "01a17e68-a4aa-7616-8c7c-93067d241f0e",
      "occurredAt": "2026-10-27T17:29:09.5465584Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialRevoked",
      "credentialCode": "CRD-KCBJFUHX",
      "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0",
      "employeeIdentification": "1020304050",
      "employeeFullName": "Andrés Restrepo",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000002",
      "accessPointName": "Parqueadero",
      "rejectionDescription": "La credencial fue revocada."
    },
    {
      "id": "01a17e68-a49c-7b4b-8fec-95d19abcdb15",
      "occurredAt": "2026-10-27T17:29:09.5323336Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "EmployeeInactive",
      "credentialCode": "CRD-KCBJFUHX",
      "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0",
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
GET /api/access-history?employeeId=01a17e68-a282-76c1-b8e1-c7a05dfcf765&result=Denied
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a17e68-a472-7d0d-8a1a-645ee6b02a48",
      "occurredAt": "2026-10-27T17:29:09.4905653Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "AccessPointDisabled",
      "credentialCode": "CRD-YBVVNDZX",
      "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
      "employeeIdentification": "1037654321",
      "employeeFullName": "Laura Gómez",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000003",
      "accessPointName": "Bodega central",
      "rejectionDescription": "El punto de acceso está inhabilitado."
    },
    {
      "id": "01a17e68-a449-7bfc-b949-ce70b47a858b",
      "occurredAt": "2026-10-27T17:29:09.4492206Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialBlocked",
      "credentialCode": "CRD-YBVVNDZX",
      "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
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
GET /api/access-history?accessPointId=0199f7a0-0000-7000-8000-000000000001&from=2026-10-27&to=2026-10-27
```

Respuesta **200**:

```json
{
  "items": [
    {
      "id": "01a17e68-a449-7bfc-b949-ce70b47a858b",
      "occurredAt": "2026-10-27T17:29:09.4492206Z",
      "movementType": "Entry",
      "result": "Denied",
      "rejectionReason": "CredentialBlocked",
      "credentialCode": "CRD-YBVVNDZX",
      "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765",
      "employeeIdentification": "1037654321",
      "employeeFullName": "Laura Gómez",
      "accessPointId": "0199f7a0-0000-7000-8000-000000000001",
      "accessPointName": "Recepción principal",
      "rejectionDescription": "La credencial está bloqueada."
    },
    {
      "id": "01a17e68-a434-720f-9af4-dd834e98c463",
      "occurredAt": "2026-10-27T17:29:09.4282262Z",
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
PUT /api/employees/01a17e68-a2b9-7913-93d2-4e2333b741f0
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
  "employeeId": "01a17e68-a282-76c1-b8e1-c7a05dfcf765"
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
  "employeeId": "01a17e68-a2b9-7913-93d2-4e2333b741f0"
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
PATCH /api/credentials/CRD-KCBJFUHX/revoke
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
PATCH /api/credentials/CRD-YBVVNDZX/block
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
  "credentialCode": "CRD-YBVVNDZX",
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
PATCH /api/access-points/01a17e68-a1bf-7e85-8b5f-93813e252f0c/disable
```

Respuesta **204** (sin contenido).

### 46. RF-06 · 400 · Inhabilitar un punto ya inhabilitado

```http
PATCH /api/access-points/01a17e68-a1bf-7e85-8b5f-93813e252f0c/disable
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
