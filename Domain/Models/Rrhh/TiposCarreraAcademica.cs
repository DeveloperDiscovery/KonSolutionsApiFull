using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_CARRERA_ACADEMICA - tipos equivalentes a la BD.</summary>
public class TiposCarreraAcademica
{
    public string COD_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
    public string DES_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
}

public class TiposCarreraAcademicaInsertDto
{
    [Required] public string COD_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
}

public class TiposCarreraAcademicaUpdateDto
{
    [Required] public string COD_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CARRERA_ACADEMICA { get; set; } = string.Empty;
}

public class TiposCarreraAcademicaSearchDto
{
    public string? Search { get; set; }
}