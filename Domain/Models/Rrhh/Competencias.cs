using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad COMPETENCIAS - tipos equivalentes a la BD.</summary>
public class Competencias
{
    public string COD_COMPETENCIA { get; set; } = string.Empty;
    public string DES_COMPETENCIA { get; set; } = string.Empty;
    public decimal? TAS_PESO_EVALUACION { get; set; }
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_COMPETENCIA { get; set; } = string.Empty;
}

public class CompetenciasInsertDto
{
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string DES_COMPETENCIA { get; set; } = string.Empty;
    public decimal? TAS_PESO_EVALUACION { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CompetenciasUpdateDto
{
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string DES_COMPETENCIA { get; set; } = string.Empty;
    public decimal? TAS_PESO_EVALUACION { get; set; }
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CompetenciasSearchDto
{
    public string? Search { get; set; }
}