using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad TIPOS_ELEMENTO_FORMULA - tipos equivalentes a la BD.</summary>
public class TiposElementoFormula
{
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    public string DES_TIPO_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public string DES_TIPO_MASCARA_FORMULA { get; set; } = string.Empty;
    public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_INPUT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string DES_CLASE_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public string DES_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
}

public class TiposElementoFormulaInsertDto
{
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_TIPO_ELEMENTO_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_MASCARA_FORMULA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_INPUT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposElementoFormulaUpdateDto
{
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_TIPO_ELEMENTO_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_MASCARA_FORMULA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_INPUT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposElementoFormulaSearchDto
{
    public string? Search { get; set; }
}