using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.TIPOS_MOVIMIENTO_TIPOS_COMPROBATE_PAGO — los tipos de
/// comprobante de pago que un tipo de movimiento acepta.
///
/// El nombre de la tabla y el de sus SPs traen "COMPROBATE" sin la N. Es así en la base, y
/// los nombres de SP se escriben tal cual o la llamada no resuelve (ver StoredProcedureNames).
/// Las clases de acá se escriben bien: el error no tiene por qué propagarse al código nuevo.
///
/// Los DES_ son de los JOIN del SP (TIPOS_MOVIMIENTO y ESTANDAR.TIPOS_COMPROBANTES_PAGO) y
/// llegan nulos si el maestro no tiene la fila, así que van anulables.</summary>
public class TiposMovimientoTiposComprobantePago
{
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public int FLG_CONTROL_CORRELATIVO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public string? DES_TIPO_COMPROBANTE_PAGO { get; set; }

    /// <summary>El formato del número, traído del tipo de comprobante por el JOIN del SP. Va
    /// acá para que la pantalla que registra un documento no tenga que ir a buscarlo a
    /// ESTANDAR.TIPOS_COMPROBANTES_PAGO, que es otro endpoint con otro permiso.</summary>
    public string? COD_PREFIJO { get; set; }
    public string? COD_PREFIJO_SUNAT { get; set; }
    public int? VAL_LONGITUD_SERIE { get; set; }
    public int? VAL_LONGITUD_NUMERO { get; set; }
}

/// <summary>Alta y modificación comparten forma: el SP de UPDATE recibe exactamente los mismos
/// parámetros que el de INSERT (lo único que cambia entre una fila y otra es
/// FLG_CONTROL_CORRELATIVO, el resto es la clave).</summary>
public class TiposMovimientoTiposComprobantePagoInsertDto
{
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public int FLG_CONTROL_CORRELATIVO { get; set; }
}

public class TiposMovimientoTiposComprobantePagoSearchDto
{
    public string? Search { get; set; }
}
