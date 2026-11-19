using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TITULOS_ACADEMICOS - tipos equivalentes a la BD.</summary>
public class TitulosAcademicos
{
    public string COD_TITULO_ACADEMICO { get; set; } = string.Empty;
    public string DES_TITULO_ACADEMICO { get; set; } = string.Empty;
}

public class TitulosAcademicosInsertDto
{
    [Required] public string COD_TITULO_ACADEMICO { get; set; } = string.Empty;
    [Required] public string DES_TITULO_ACADEMICO { get; set; } = string.Empty;
}

public class TitulosAcademicosUpdateDto
{
    [Required] public string COD_TITULO_ACADEMICO { get; set; } = string.Empty;
    [Required] public string DES_TITULO_ACADEMICO { get; set; } = string.Empty;
}

public class TitulosAcademicosSearchDto
{
    public string? Search { get; set; }
}