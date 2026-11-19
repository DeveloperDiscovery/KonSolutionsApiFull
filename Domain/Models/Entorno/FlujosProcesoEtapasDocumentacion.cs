using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad FLUJOS_PROCESO_ETAPAS_DOCUMENTACION - tipos equivalentes a la BD.</summary>
public class FlujosProcesoEtapasDocumentacion
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_ETAPA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_ETAPA { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
}

public class FlujosProcesoEtapasDocumentacionInsertDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_ETAPA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_PATH_FILE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoEtapasDocumentacionUpdateDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_ETAPA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_PATH_FILE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoEtapasDocumentacionSearchDto
{
    public string? Search { get; set; }
}