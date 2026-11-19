using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTAROS_ALMACENES_INVENTARIADORES - tipos equivalentes a la BD.</summary>
public class InventarosAlmacenesInventariadores
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_INVENTARIADOR { get; set; }
    public string COD_TIPO_INVENTARIO { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    public bool? FLG_MANUAL { get; set; }
    public int? COD_ESTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_INVENTARIO { get; set; } = string.Empty;
}

public class InventarosAlmacenesInventariadoresInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_INVENTARIADOR { get; set; }
    [Required] public string COD_TIPO_INVENTARIO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    public bool? FLG_MANUAL { get; set; }
    public int? COD_ESTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
}

public class InventarosAlmacenesInventariadoresUpdateDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_INVENTARIADOR { get; set; }
    [Required] public string COD_TIPO_INVENTARIO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_TERMINO { get; set; }
    public bool? FLG_MANUAL { get; set; }
    public int? COD_ESTADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
}

public class InventarosAlmacenesInventariadoresSearchDto
{
    public string? Search { get; set; }
}