using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad ORDEN_PRODUCCION_ITEMS - tipos equivalentes a la BD.</summary>
public class OrdenProduccionItems
{
    public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_RUTA_PRODUCCION { get; set; }
    public decimal? CAN_PRODUCIR { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIR_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public bool? FLG_REPOSICION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_RUTA_PRODUCCION { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
}

public class OrdenProduccionItemsInsertDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_RUTA_PRODUCCION { get; set; }
    public decimal? CAN_PRODUCIR { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIR_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public bool? FLG_REPOSICION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OrdenProduccionItemsUpdateDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public int? COD_RUTA_PRODUCCION { get; set; }
    public decimal? CAN_PRODUCIR { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCIR_SECUNDARIA { get; set; }
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public bool? FLG_REPOSICION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class OrdenProduccionItemsSearchDto
{
    public string? Search { get; set; }
}