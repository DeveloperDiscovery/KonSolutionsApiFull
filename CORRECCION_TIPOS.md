# Corrección de tipos de datos — KonSolutions API

## Qué se corrigió

Los tipos C# ahora respetan los tipos reales de la base de datos, leídos de las
330 definiciones de tipos personalizados (CREATE TYPE) del script.

## Tabla de equivalencias aplicada

| Tipo base SQL | Tipo C# |
|---------------|---------|
| int, smallint, tinyint | int |
| bigint | long |
| numeric, decimal, money | decimal |
| float, real | double |
| bit | bool |
| date, datetime, datetime2 | DateTime |
| image, varbinary, binary | byte[] |
| nvarchar, varchar, char, text | string |

## Campos IDENTITY → int? (nullable)

Los 56 campos identity ahora son nullable (int?/decimal?) para soportar el patrón
de tus SP: cuando llega null en el INSERT, el SP genera el ID automáticamente.

Campos identity detectados:
- COD_AREA  (tinyint → int?)
- COD_CARGO  (tinyint → int?)
- COD_CLASE_ELEMENTO_FORMULA  (smallint → int?)
- COD_CONCEPTO_REMUNERATIVO  (smallint → int?)
- COD_CONDICION_PAGO  (smallint → int?)
- COD_ENTIDAD  (numeric → decimal?)
- COD_FORMA_PAGO  (smallint → int?)
- COD_GRADO_CONFIDENCIABILIDAD  (tinyint → int?)
- COD_GRUPO_PLANEAMIENTO  (smallint → int?)
- COD_IMPACTO_RIESGO  (tinyint → int?)
- COD_INDICE  (int → int?)
- COD_PROBABILIDAD_RIESGO  (tinyint → int?)
- COD_REGIMEN_TRIBUTARIO  (smallint → int?)
- COD_RUTA_PRODUCCION  (int → int?)
- COD_TAREA  (tinyint → int?)
- COD_TIPO_CARGO  (tinyint → int?)
- COD_TIPO_DESCUENTO  (smallint → int?)
- COD_TIPO_DIRECCION  (smallint → int?)
- COD_TIPO_DOCUMENTO_IDENTIDAD  (smallint → int?)
- COD_TIPO_ESTADO  (smallint → int?)
- COD_TIPO_MEDIO_CONTACTO  (smallint → int?)
- COD_TIPO_MOTIVO  (smallint → int?)
- COD_TIPO_PEDIDO_COMERCIAL  (smallint → int?)
- COD_TIPO_PRECIO  (smallint → int?)
- NUM_ANO  (int → int?)
- NUM_APROBACION  (int → int?)
- NUM_AUTORIZACION  (int → int?)
- NUM_CATALOGO_PRECIOS  (int → int?)
- NUM_CONTENEDOR_PRODUCCION  (int → int?)
- NUM_CONTRATO  (int → int?)
- NUM_CONTROL_CALIDAD  (int → int?)
- NUM_DOCUMENTO_OFICIAL  (int → int?)
- NUM_ENCUESTA  (smallint → int?)
- NUM_ENTREGA_IMPLEMENTOS  (? → ??)
- NUM_ESTRUCTURA_ESPECIFICACION_TECNICA  (int → int?)
- NUM_EVALUACION_DESEMPENO  (? → ??)
- NUM_INVENTARIO  (smallint → int?)
- NUM_MANTENIMIENTO_INDUSTRIAL  (int → int?)
- NUM_MANUAL_FUNCIONES  (smallint → int?)
- NUM_NIVEL_EVALUACION  (tinyint → int?)
- NUM_PEDIDO_COMERCIAL  (int → int?)
- NUM_PERIODO  (smallint → int?)
- NUM_PLANILLA_REMUNERATIVA  (int → int?)
- NUM_PROGRAMA  (int → int?)
- NUM_PROYECTO  (int → int?)
- NUM_READ_TAG  (int → int?)
- NUM_REGISTRO_PRODUCCION  (int → int?)
- NUM_RENDICION_CUENTAS  (smallint → int?)
- NUM_REQUERIMIENTO_STOCK  (int → int?)
- NUM_RESERVA_STOCK  (int → int?)
- NUM_RUTA_ARCHIVO  (smallint → int?)
- NUM_SECUENCIA  (smallint → int?)
- NUM_SOLICITUD  (int → int?)
- NUM_SOLICITUD_PERSONAL  (? → ??)
- NUM_SOLICITUD_RECLUTAMIENTO  (? → ??)
- NUM_TICKET  (int → int?)

## Ejemplos de corrección

| Campo | Tipo SQL (UDT) | Antes | Ahora |
|-------|----------------|-------|-------|
| COD_TIPO_ESTADO | smallint (identity) | string | int? |
| COD_ENTIDAD | numeric(15,0) (identity) | string | decimal? |
| COD_AREA | tinyint (identity) | string | int? |
| COD_USUARIO | varchar(50) | string | string (correcto) |
| VAL_PRECIO | numeric | string | decimal |
| FLG_LOGICO | bit | bool | bool? |
| IMG_PICTURE | image | byte[]? | byte[]? |
| FEC_DATE | date | DateTime? | DateTime |

## Reglas de nullabilidad

- Campos IDENTITY: nullable siempre (permiten null en INSERT).
- Tipos de valor (int, decimal, bool, DateTime) en entidades de lectura: nullable
  para tolerar NULLs que vengan de la BD.
- Campos string obligatorios en DTOs: [Required] + = string.Empty.
- Las firmas de PK en controllers/servicios/repositorios se ajustaron al tipo real
  (244 entidades tenían PK no-string).

---

## Corrección adicional: parámetros exactos por SP

Se detectó que la API enviaba parámetros de auditoría (COD_USUARIO_REGISTRO,
COD_ESTACION_REGISTRO) a TODOS los SP de INSERT/UPDATE, pero solo 281 de 409
los declaran. Los otros 128 (catálogos simples como TIPOS_ESTADOS) no los piden,
y SQL Server rechazaba con "tiene demasiados argumentos".

Ahora cada repositorio envía EXACTAMENTE los parámetros que su SP declara:
- Lee la firma real del SP desde el script.
- Inyecta auditoría (usuario/estación/fecha) SOLO si el SP la declara.
- Los demás campos vienen del DTO.

## Corrección adicional: colisión de nombres entre módulos

La entidad TIPOS_TRANSPORTE existe en COMERCIAL y en LOGISTICA con campos
distintos (COMERCIAL: IMG_ICONO; LOGISTICA: FLG_TERRESTRE/MARITIMO/FLUVIAL/AEREO).
Compartían el mismo nombre de clase y colisionaban. Se desdoblaron en:
- ComercialTiposTransporte  → ruta /api/v1/comercial/tipos-transporte
- LogisticaTiposTransporte  → ruta /api/v1/logistica/tipos-transporte

---

## Corrección adicional: firmas PK desincronizadas (error CS0535)

70 entidades con PK que incluía un campo de tipo FECHA (UDT FEC_DATE) tenían un
desajuste: la interfaz declaraba el parámetro como DateTime pero el repositorio lo
implementaba como string. C# lo reportaba como "no implementa el miembro de interfaz".

Causa: al ajustar los tipos PK, el tipo se resolvía por el NOMBRE del campo
(FEC_ASIGNACION_INICIO no es un UDT) en vez de por el TIPO del parámetro del SP
(FEC_DATE → DateTime). Corregido leyendo el tipo declarado del parámetro.

Ahora interface, repository, service y controller comparten exactamente la misma
firma para cada PK en las 410 entidades.

---

## Corrección adicional: firmas sin auditoría innecesaria

128 entidades cuyo SP de INSERT NO declara parámetros de auditoría
(COD_USUARIO_REGISTRO, etc.) tenían en su firma `string codUsuario, string codEstacion`
que nunca se usaban (código muerto). Ejemplo: TiposEvaluacion.

Se limpiaron las firmas en las 4 capas (interface, repository, service, controller):
- Antes: InsertAsync(XxxInsertDto dto, string codUsuario, string codEstacion)
- Ahora: InsertAsync(XxxInsertDto dto)

Las 281 entidades cuyo SP SÍ pide auditoría conservan la inyección desde el token.

## Nota sobre UPDATE
El sistema no tiene SP _UPDATE separados: los SP _INSERT funcionan como UPSERT
(intentan UPDATE y si @@ROWCOUNT=0 hacen INSERT). Por eso solo existe el endpoint
POST que cubre ambos casos.
