using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad CLASES_MOVIMIENTO - tipos equivalentes a la BD.</summary>
public class ClasesMovimiento
{
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string DES_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    /// <summary>La clase aplica a movimientos de ALMACÉN.</summary>
    public bool? FLG_ALMACENES { get; set; }
    /// <summary>La clase aplica a movimientos de PRODUCCIÓN. No son excluyentes: una clase
    /// puede servir a los dos ámbitos.</summary>
    public bool? FLG_PRODUCCION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class ClasesMovimientoInsertDto
{
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public bool? FLG_ALMACENES { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ClasesMovimientoUpdateDto
{
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public bool? FLG_ALMACENES { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ClasesMovimientoSearchDto
{
    public string? Search { get; set; }
}