using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.SUCURSALES_TIPOS_COMPROBANTE_PAGO — series y correlativos de
/// comprobante habilitados por sucursal. Hija de ESTANDAR.SUCURSALES.
/// PK compuesta: COD_SUCURSAL + COD_TIPO_COMPROBANTE_PAGO.</summary>
public class SucursalesTiposComprobantePago
{
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public short? NUM_SERIE_COMPROBANTE { get; set; }
    public int? NUM_CORRELATIVO_ULTIMO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura (JOINs). El typo DES_DIRECCCION de la columna ya está corregido en la BD.
    public string? DES_DIRECCION { get; set; }
    public string? DES_LINK_PATH_FACTURACION { get; set; }
    public string? DES_SUCURSAL { get; set; }
    public string? DES_TOKEN_FACTURACION { get; set; }
    public string? DES_TIPO_COMPROBANTE_PAGO { get; set; }
}

/// <summary>El SP _INSERT hace upsert por (sucursal, tipo de comprobante): ambos códigos son
/// obligatorios en Crear y Modificar. Usuario y estación salen del token, no del cuerpo.</summary>
public class SucursalesTiposComprobantePagoInsertDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public short? NUM_SERIE_COMPROBANTE { get; set; }
    public int? NUM_CORRELATIVO_ULTIMO { get; set; }
}

public class SucursalesTiposComprobantePagoSearchDto
{
    public string? COD_SUCURSAL { get; set; }
    public string? Search { get; set; }
}
