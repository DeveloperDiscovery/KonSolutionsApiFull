using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad SUB_CANALES_VENTA - tipos equivalentes a la BD.</summary>
public class SubCanalesVenta
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public bool? FLG_PRODUCTO { get; set; }
    public bool? FLG_COMBO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CANAL_VENTA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class SubCanalesVentaInsertDto
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public bool? FLG_PRODUCTO { get; set; }
    public bool? FLG_COMBO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubCanalesVentaUpdateDto
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public bool? FLG_PRODUCTO { get; set; }
    public bool? FLG_COMBO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubCanalesVentaSearchDto
{
    public string? Search { get; set; }
}