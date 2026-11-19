using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_PROBABILIDAD_RIESGO - tipos equivalentes a la BD.</summary>
public class ProyectosProbabilidadRiesgo
{
    public int? COD_PROBABILIDAD_RIESGO { get; set; }
    public string DES_PROBABILIDAD_RIESGO { get; set; } = string.Empty;
    public decimal? VAL_PROBABILIDAD { get; set; }
}

public class ProyectosProbabilidadRiesgoInsertDto
{
    public int? COD_PROBABILIDAD_RIESGO { get; set; }
    [Required] public string DES_PROBABILIDAD_RIESGO { get; set; } = string.Empty;
    public decimal? VAL_PROBABILIDAD { get; set; }
}

public class ProyectosProbabilidadRiesgoUpdateDto
{
    public int? COD_PROBABILIDAD_RIESGO { get; set; }
    [Required] public string DES_PROBABILIDAD_RIESGO { get; set; } = string.Empty;
    public decimal? VAL_PROBABILIDAD { get; set; }
}

public class ProyectosProbabilidadRiesgoSearchDto
{
    public string? Search { get; set; }
}