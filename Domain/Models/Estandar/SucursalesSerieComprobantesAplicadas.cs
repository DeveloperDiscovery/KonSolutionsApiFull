using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad SUCURSALES_SERIE_COMPROBANTES_APLICADAS - tipos equivalentes a la BD.</summary>
public class SucursalesSerieComprobantesAplicadas
{
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string COD_TIPO_COMPROBANTE_PAGO_APLICADO { get; set; } = string.Empty;
    public int? NUM_SERIE_COMPROBANTE { get; set; }
    public decimal? NUM_ULTIMO_CORRELATIVO { get; set; }
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
}

public class SucursalesSerieComprobantesAplicadasInsertDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO_APLICADO { get; set; } = string.Empty;
    public int? NUM_SERIE_COMPROBANTE { get; set; }
    public decimal? NUM_ULTIMO_CORRELATIVO { get; set; }
}

public class SucursalesSerieComprobantesAplicadasUpdateDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPROBANTE_PAGO_APLICADO { get; set; } = string.Empty;
    public int? NUM_SERIE_COMPROBANTE { get; set; }
    public decimal? NUM_ULTIMO_CORRELATIVO { get; set; }
}

public class SucursalesSerieComprobantesAplicadasSearchDto
{
    public string? Search { get; set; }
}