using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad FLUJOS_PROCESO_MEMORIA - tipos equivalentes a la BD.</summary>
public class FlujosProcesoMemoria
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string DES_MEMORIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_FLUJO_PROCESO { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
}

public class FlujosProcesoMemoriaInsertDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoMemoriaUpdateDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoMemoriaSearchDto
{
    public string? Search { get; set; }
}