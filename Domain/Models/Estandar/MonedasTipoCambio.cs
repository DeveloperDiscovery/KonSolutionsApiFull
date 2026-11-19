using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad MONEDAS_TIPO_CAMBIO - tipos equivalentes a la BD.</summary>
public class MonedasTipoCambio
{
    public string COD_MONEDA { get; set; } = string.Empty;
    public DateTime? FEC_TIPO_CAMBIO { get; set; }
    public decimal? FAC_COMERCIAL_COMPRA { get; set; }
    public decimal? FAC_COMERCIAL_VENTA { get; set; }
    public decimal? FAC_SUNAT_COMPRA { get; set; }
    public decimal? FAC_SUNAT_VENTA { get; set; }
    public decimal? FAC_PARALELO_COMPRA { get; set; }
    public decimal? FAC_PARALELO_VENTA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_MONEDA { get; set; } = string.Empty;
}

public class MonedasTipoCambioInsertDto
{
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public DateTime? FEC_TIPO_CAMBIO { get; set; }
    public decimal? FAC_COMERCIAL_COMPRA { get; set; }
    public decimal? FAC_COMERCIAL_VENTA { get; set; }
    public decimal? FAC_SUNAT_COMPRA { get; set; }
    public decimal? FAC_SUNAT_VENTA { get; set; }
    public decimal? FAC_PARALELO_COMPRA { get; set; }
    public decimal? FAC_PARALELO_VENTA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MonedasTipoCambioUpdateDto
{
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public DateTime? FEC_TIPO_CAMBIO { get; set; }
    public decimal? FAC_COMERCIAL_COMPRA { get; set; }
    public decimal? FAC_COMERCIAL_VENTA { get; set; }
    public decimal? FAC_SUNAT_COMPRA { get; set; }
    public decimal? FAC_SUNAT_VENTA { get; set; }
    public decimal? FAC_PARALELO_COMPRA { get; set; }
    public decimal? FAC_PARALELO_VENTA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MonedasTipoCambioSearchDto
{
    public string? Search { get; set; }
}