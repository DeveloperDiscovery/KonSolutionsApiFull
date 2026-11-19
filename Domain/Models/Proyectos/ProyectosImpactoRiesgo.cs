using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_IMPACTO_RIESGO - tipos equivalentes a la BD.</summary>
public class ProyectosImpactoRiesgo
{
    public int? COD_IMPACTO_RIESGO { get; set; }
    public string DES_IMPACTO_RIESGO { get; set; } = string.Empty;
    public decimal? VAL_PROBABILIDAD { get; set; }
}

public class ProyectosImpactoRiesgoInsertDto
{
    public int? COD_IMPACTO_RIESGO { get; set; }
    [Required] public string DES_IMPACTO_RIESGO { get; set; } = string.Empty;
    public decimal? VAL_PROBABILIDAD { get; set; }
}

public class ProyectosImpactoRiesgoUpdateDto
{
    public int? COD_IMPACTO_RIESGO { get; set; }
    [Required] public string DES_IMPACTO_RIESGO { get; set; } = string.Empty;
    public decimal? VAL_PROBABILIDAD { get; set; }
}

public class ProyectosImpactoRiesgoSearchDto
{
    public string? Search { get; set; }
}