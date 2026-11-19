using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad CARRERAS_ACADEMICAS - tipos equivalentes a la BD.</summary>
public class CarrerasAcademicas
{
    public string COD_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string DES_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string COD_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
}

public class CarrerasAcademicasInsertDto
{
    [Required] public string COD_CARRERA_ACADEMICA { get; set; } = string.Empty;
    [Required] public string DES_CARRERA_ACADEMICA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CarrerasAcademicasUpdateDto
{
    [Required] public string COD_CARRERA_ACADEMICA { get; set; } = string.Empty;
    [Required] public string DES_CARRERA_ACADEMICA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CarrerasAcademicasSearchDto
{
    public string? Search { get; set; }
}