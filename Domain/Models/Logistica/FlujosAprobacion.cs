using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.FLUJOS_APROBACION — maestro del flujo de aprobación.
/// NUM_FLUJO_APROBACION lo genera la BD (IDENTITY): en el alta viaja nulo.</summary>
public class FlujosAprobacion
{
    public short? NUM_FLUJO_APROBACION { get; set; }
    public string? DES_FLUJO_APROBACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura: los resuelve el JOIN a ESTANDAR.ESTADOS de los SP.
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
}

public class FlujosAprobacionInsertDto
{
    public short? NUM_FLUJO_APROBACION { get; set; }
    [Required] public string DES_FLUJO_APROBACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class FlujosAprobacionUpdateDto
{
    [Required] public short? NUM_FLUJO_APROBACION { get; set; }
    [Required] public string DES_FLUJO_APROBACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class FlujosAprobacionSearchDto
{
    public string? Search { get; set; }
}


// ─────────────────────────────────────────────────────────────────────────────
// Detalle 1: niveles de aprobación (Área + Cargo)
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>Entidad LOGISTICA.FLUJOS_APROBACION_AREA_CARGO. PK compuesta
/// (NUM_FLUJO_APROBACION, COD_AREA, COD_CARGO). DES_AREA/DES_CARGO son de solo lectura:
/// los resuelven los JOIN de los SP.</summary>
public class FlujosAprobacionAreaCargo
{
    public short? NUM_FLUJO_APROBACION { get; set; }
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public int? NUM_ORDEN_APROBACION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? VAL_MONTO_MINIMO { get; set; }
    public decimal? VAL_MONTO_MAXIMO { get; set; }
    public bool? FLG_ULTIMO { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura (JOIN).
    public string? DES_AREA { get; set; }
    public string? DES_CARGO { get; set; }
    public string? DES_MONEDA { get; set; }
}

public class FlujosAprobacionAreaCargoInsertDto
{
    [Required] public short? NUM_FLUJO_APROBACION { get; set; }
    [Required] public int? COD_AREA { get; set; }
    [Required] public int? COD_CARGO { get; set; }
    public int? NUM_ORDEN_APROBACION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? VAL_MONTO_MINIMO { get; set; }
    public decimal? VAL_MONTO_MAXIMO { get; set; }
    public bool? FLG_ULTIMO { get; set; }
    public bool? FLG_HABILITADO { get; set; }
}

public class FlujosAprobacionAreaCargoUpdateDto
{
    [Required] public short? NUM_FLUJO_APROBACION { get; set; }
    [Required] public int? COD_AREA { get; set; }
    [Required] public int? COD_CARGO { get; set; }
    public int? NUM_ORDEN_APROBACION { get; set; }
    public string? COD_MONEDA { get; set; }
    public decimal? VAL_MONTO_MINIMO { get; set; }
    public decimal? VAL_MONTO_MAXIMO { get; set; }
    public bool? FLG_ULTIMO { get; set; }
    public bool? FLG_HABILITADO { get; set; }
}

/// <summary>Copiar los niveles de aprobación de un flujo a otro.</summary>
public class FlujosAprobacionAreaCargoCopyDto
{
    [Required] public short? NUM_FLUJO_APROBACION { get; set; }
    [Required] public short? NUM_FLUJO_APROBACION_DESTINO { get; set; }
    /// <summary>false = solo agrega al destino los niveles que le falten (no borra nada).
    /// true = vacía el destino y lo deja idéntico al origen.</summary>
    public bool FLG_REEMPLAZAR { get; set; }
}

public class FlujosAprobacionAreaCargoSearchDto
{
    public short? NUM_FLUJO_APROBACION { get; set; }
    public int? COD_AREA { get; set; }
    public string? Search { get; set; }
}


// ─────────────────────────────────────────────────────────────────────────────
// Detalle 2: tipos de operación que disparan el flujo
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>Entidad LOGISTICA.FLUJOS_APROBACION_TIPOS_OPERACION. PK compuesta
/// (NUM_FLUJO_APROBACION, COD_CLASE_OPERACION, COD_TIPO_OPERACION).
/// Tabla de ASIGNACIÓN: no tiene columnas de datos propias más allá de la auditoría, por eso
/// su SP de alta es un upsert y no existe _UPDATE.</summary>
public class FlujosAprobacionTiposOperacion
{
    public short? NUM_FLUJO_APROBACION { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // Solo lectura (JOIN a CLASES_OPERACION / TIPOS_OPERACION).
    public string? DES_CLASE_OPERACION { get; set; }
    public string? DES_TIPO_OPERACION { get; set; }
}

public class FlujosAprobacionTiposOperacionInsertDto
{
    [Required] public short? NUM_FLUJO_APROBACION { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
}

public class FlujosAprobacionTiposOperacionSearchDto
{
    public short? NUM_FLUJO_APROBACION { get; set; }
    public string? COD_CLASE_OPERACION { get; set; }
    public string? Search { get; set; }
}
