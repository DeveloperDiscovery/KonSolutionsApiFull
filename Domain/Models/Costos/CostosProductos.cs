using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Costos;

/// <summary>Entidad COSTOS_PRODUCTOS - tipos equivalentes a la BD.</summary>
public class CostosProductos
{
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? VAL_UNITARIO_COSTO { get; set; }
    public decimal? VAL_TOTAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_ABREVIADA { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;
}

public class CostosProductosInsertDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? VAL_UNITARIO_COSTO { get; set; }
    public decimal? VAL_TOTAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosProductosUpdateDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIA { get; set; }
    public decimal? VAL_UNITARIO_COSTO { get; set; }
    public decimal? VAL_TOTAL_COSTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CostosProductosSearchDto
{
    public string? Search { get; set; }
}