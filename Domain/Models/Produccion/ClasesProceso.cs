using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad CLASES_PROCESO - tipos equivalentes a la BD.</summary>
public class ClasesProceso
{
    public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    public string DES_CLASE_PROCESO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_GENERA_GRUPO_REQUERIMIENTO { get; set; }
}

public class ClasesProcesoInsertDto
{
    [Required] public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_PROCESO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_GENERA_GRUPO_REQUERIMIENTO { get; set; }
}

public class ClasesProcesoUpdateDto
{
    [Required] public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_PROCESO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_GENERA_GRUPO_REQUERIMIENTO { get; set; }
}

public class ClasesProcesoSearchDto
{
    public string? Search { get; set; }
}