using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_PRODUCTO - tipos equivalentes a la BD.</summary>
public class TiposProducto
{
    public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_COMPRADO { get; set; }
    public bool? FLG_SERVICIO { get; set; }
    public bool? FLG_GASTOS { get; set; }
    public bool? FLG_INDUSTRIALIZADO { get; set; }
    public bool? FLG_GENERICO { get; set; }
    public bool? FLG_DESARROLLO { get; set; }
    public bool? FLG_GASTO_ADUANERO { get; set; }
    public bool? FLG_FINANCIERO { get; set; }
    public int? COD_TIPO_ESTADO_INICIAL { get; set; }
    public int? COD_ESTADO_INICIAL { get; set; }
    public string? DES_TIPO_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_FORECOLOR { get; set; }
}

// Único DTO de escritura: el SP hace upsert (UPDATE, si no encuentra -> INSERT), igual que
// el resto de módulos de este sistema — ya no existe un PROC_..._UPDATE separado.
public class TiposProductoInsertDto
{
    [Required] public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_COMPRADO { get; set; }
    public bool? FLG_SERVICIO { get; set; }
    public bool? FLG_GASTOS { get; set; }
    public bool? FLG_INDUSTRIALIZADO { get; set; }
    public bool? FLG_GENERICO { get; set; }
    public bool? FLG_DESARROLLO { get; set; }
    public bool? FLG_GASTO_ADUANERO { get; set; }
    public bool? FLG_FINANCIERO { get; set; }
    public int? COD_TIPO_ESTADO_INICIAL { get; set; }
    public int? COD_ESTADO_INICIAL { get; set; }
}

public class TiposProductoSearchDto
{
    public string? Search { get; set; }
}
