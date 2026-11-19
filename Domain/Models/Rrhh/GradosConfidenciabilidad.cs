using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad GRADOS_CONFIDENCIABILIDAD - tipos equivalentes a la BD.</summary>
public class GradosConfidenciabilidad
{
    public int? COD_GRADO_CONFIDENCIABILIDAD { get; set; }
    public string DES_GRADO_CONFIDENCIABILIDAD { get; set; } = string.Empty;
}

public class GradosConfidenciabilidadInsertDto
{
    public int? COD_GRADO_CONFIDENCIABILIDAD { get; set; }
    [Required] public string DES_GRADO_CONFIDENCIABILIDAD { get; set; } = string.Empty;
}

public class GradosConfidenciabilidadUpdateDto
{
    public int? COD_GRADO_CONFIDENCIABILIDAD { get; set; }
    [Required] public string DES_GRADO_CONFIDENCIABILIDAD { get; set; } = string.Empty;
}

public class GradosConfidenciabilidadSearchDto
{
    public string? Search { get; set; }
}