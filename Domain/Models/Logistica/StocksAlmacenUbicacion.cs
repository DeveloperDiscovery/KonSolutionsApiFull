using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad STOCKS_ALMACEN_UBICACION - tipos equivalentes a la BD.</summary>
public class StocksAlmacenUbicacion
{
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIO { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public decimal? CAN_RESERVADA { get; set; }
    public decimal? CAN_RESERVADA_SECUNDARIO { get; set; }
    public decimal? CAN_DISPONIBLE { get; set; }
    public decimal? CAN_DISPONIBLE_SECUNDARIO { get; set; }
    public string DES_COLUMNA { get; set; } = string.Empty;
    public string DES_FILA { get; set; } = string.Empty;
    public string DES_OTRA_UBICACION { get; set; } = string.Empty;
    public string DES_RACK { get; set; } = string.Empty;
    public string DES_UBICACION_ALMACEN { get; set; } = string.Empty;
    public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_INMOBILIZADO { get; set; }
    public decimal? CAN_INMOBILIZADO_SECUNDARIO { get; set; }
    public decimal? CAN_TRANSITO { get; set; }
    public decimal? CAN_TRANSITO_SECUNDARIO { get; set; }
}

public class StocksAlmacenUbicacionInsertDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public decimal? CAN_RESERVADA { get; set; }
    public decimal? CAN_RESERVADA_SECUNDARIO { get; set; }
    public decimal? CAN_DISPONIBLE { get; set; }
    public decimal? CAN_DISPONIBLE_SECUNDARIO { get; set; }
    public decimal? CAN_INMOBILIZADO { get; set; }
    public decimal? CAN_INMOBILIZADO_SECUNDARIO { get; set; }
    public decimal? CAN_TRANSITO { get; set; }
    public decimal? CAN_TRANSITO_SECUNDARIO { get; set; }
}

public class StocksAlmacenUbicacionUpdateDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_UBICACION_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO_SECUNDARIO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIO { get; set; } = string.Empty;
    public decimal? CAN_RESERVADA { get; set; }
    public decimal? CAN_RESERVADA_SECUNDARIO { get; set; }
    public decimal? CAN_DISPONIBLE { get; set; }
    public decimal? CAN_DISPONIBLE_SECUNDARIO { get; set; }
    public decimal? CAN_INMOBILIZADO { get; set; }
    public decimal? CAN_INMOBILIZADO_SECUNDARIO { get; set; }
    public decimal? CAN_TRANSITO { get; set; }
    public decimal? CAN_TRANSITO_SECUNDARIO { get; set; }
}

public class StocksAlmacenUbicacionSearchDto
{
    public string? Search { get; set; }
}