using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad FLUJOS_PROCESO_DOCUMENTACION - tipos equivalentes a la BD.</summary>
public class FlujosProcesoDocumentacion
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_FLUJO_PROCESO { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
}

public class FlujosProcesoDocumentacionInsertDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_PATH_FILE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoDocumentacionUpdateDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_PATH_FILE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoDocumentacionSearchDto
{
    public string? Search { get; set; }
}