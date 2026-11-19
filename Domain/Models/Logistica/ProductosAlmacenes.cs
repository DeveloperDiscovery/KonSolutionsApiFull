using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad PRODUCTOS_ALMACENES - tipos equivalentes a la BD.</summary>
public class ProductosAlmacenes
{
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public decimal? CAN_STOCK_MINIMO { get; set; }
    public decimal? CAN_STOCK_MAXIMO { get; set; }
    public int? CAN_LEAD_TIME_DIAS { get; set; }
    /// <summary>Consumo diario del producto en ese almacén. El nombre lleva el error de
    /// tipeo que tiene la columna en la BD (CONUSMO, no CONSUMO): Dapper mapea por nombre
    /// exacto, así que corregirlo acá rompería el mapeo.</summary>
    public decimal? CAN_CONUSMO_DIARIO { get; set; }
    public decimal? CAN_STOCK_REQUERIR { get; set; }
    /// <summary>Requerimiento generado para reponer este producto en este almacén. De solo
    /// lectura EN PANTALLA (lo escribe PROC_..._REQUERIMIENTO, no el usuario), pero sí viaja
    /// en el alta/modificación: los SP lo declaran como parámetro obligatorio, así que se
    /// reenvía el valor que vino de la BD para que el UPDATE no lo pise con NULL.</summary>
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    /// <summary>Estado del REQUERIMIENTO generado (JOIN a REQUERIMIENTOS_STOCK y a
    /// ESTANDAR.ESTADOS), no de esta fila: PRODUCTOS_ALMACENES no tiene estado propio.
    /// Los colores salen configurados de la BD, igual que en el resto de las grillas.</summary>
    public int? COD_ESTADO { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public bool? FLG_REQUERIMIENTO_AUTOMATICO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    // Desnormalizados del SP (LEFT JOIN a ALMACENES y a PRODUCTOS).
    public string DES_ABREVIADA { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;
    public string DES_POLIGONO { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
}

public class ProductosAlmacenesInsertDto
{
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public decimal? CAN_STOCK_MINIMO { get; set; }
    public decimal? CAN_STOCK_MAXIMO { get; set; }
    public int? CAN_LEAD_TIME_DIAS { get; set; }
    public decimal? CAN_CONUSMO_DIARIO { get; set; }
    public decimal? CAN_STOCK_REQUERIR { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public bool? FLG_REQUERIMIENTO_AUTOMATICO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosAlmacenesUpdateDto
{
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public decimal? CAN_STOCK_MINIMO { get; set; }
    public decimal? CAN_STOCK_MAXIMO { get; set; }
    public int? CAN_LEAD_TIME_DIAS { get; set; }
    public decimal? CAN_CONUSMO_DIARIO { get; set; }
    public decimal? CAN_STOCK_REQUERIR { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public bool? FLG_REQUERIMIENTO_AUTOMATICO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProductosAlmacenesSearchDto
{
    public string? Search { get; set; }
}