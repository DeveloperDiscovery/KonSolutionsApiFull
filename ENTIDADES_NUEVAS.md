# Entidades nuevas agregadas (del script actualizado)

Se compararon los 2572 SP del script nuevo contra la API existente.
Se detectaron y completaron 9 entidades nuevas del módulo SEGURIDAD que
tenían SP en el script pero no estaban implementadas en la API:

| Entidad | Ruta del endpoint |
|---------|-------------------|
| Aplicaciones | api/v1/seguridad/aplicaciones |
| AplicacionesOpciones | api/v1/seguridad/aplicaciones-opciones |
| AplicacionesOpcionesBotones | api/v1/seguridad/aplicaciones-opciones-botones |
| BotonesEstandar | api/v1/seguridad/botones-estandar |
| ComplejidadPassword | api/v1/seguridad/complejidad-password |
| PerfilesUsuario | api/v1/seguridad/perfiles-usuario |
| RolesUsuario | api/v1/seguridad/roles-usuario |
| RolesUsuarioAplicaciones | api/v1/seguridad/roles-usuario-aplicaciones |
| TiposOpciones | api/v1/seguridad/tipos-opciones |

Cada entidad se generó con las 4 capas (Controller, Service, Repository,
Interface) + Model/DTOs, siguiendo el mismo patrón de la API existente,
con cada acción presente en su SP (SHOWALL, SHOWBYID, SHOWCOMBOX, SEARCH,
INSERT, DELETE). Se registraron en StoredProcedureNames.cs (SCHEMASeguridad)
y en Program.cs (DI).

NOTA: PerfilesUsuario incluye el endpoint /combobox que el frontend web
necesitaba (antes daba 404 al cargar el formulario de usuarios).

Las entidades TIPOS_TRANSPORTE del script ya existían en la API como
ComercialTiposTransporte y LogisticaTiposTransporte (desdobladas por colisión),
así que no se regeneraron.

---

## Fix: error 400 al guardar (campos de auditoría [Required])

Los DTOs tenían COD_USUARIO_REGISTRO, COD_ESTACION_REGISTRO y FEC_* marcados
como [Required]. Pero esos campos NO los envía el cliente: la API los inyecta
desde el token JWT (GetCodUsuario / GetEstacion). Al no recibirlos, ModelState
fallaba con 400 "Operación rechazada" antes de ejecutar el SP.

Se quitó [Required] de los 6 campos de auditoría en los 286 DTOs afectados
(1144 campos). Ahora el guardado pasa la validación y la API rellena la auditoría.
