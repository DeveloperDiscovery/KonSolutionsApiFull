using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Controlcalidad;

/// <summary>Entidad CLASES_DEFECTOS_TECNICOS - tipos equivalentes a la BD.</summary>
public class ClasesDefectosTecnicos
{
    public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    public string DES_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
}

public class ClasesDefectosTecnicosInsertDto
{
    [Required] public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
}

public class ClasesDefectosTecnicosUpdateDto
{
    [Required] public string COD_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_DEFECTO_TECNICO { get; set; } = string.Empty;
}

public class ClasesDefectosTecnicosSearchDto
{
    public string? Search { get; set; }
}