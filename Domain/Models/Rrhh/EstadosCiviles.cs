using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad ESTADOS_CIVILES - tipos equivalentes a la BD.</summary>
public class EstadosCiviles
{
    public string COD_ESTADO_CIVIL { get; set; } = string.Empty;
    public string DES_ESTADO_CIVIL { get; set; } = string.Empty;
}

public class EstadosCivilesInsertDto
{
    [Required] public string COD_ESTADO_CIVIL { get; set; } = string.Empty;
    [Required] public string DES_ESTADO_CIVIL { get; set; } = string.Empty;
}

public class EstadosCivilesUpdateDto
{
    [Required] public string COD_ESTADO_CIVIL { get; set; } = string.Empty;
    [Required] public string DES_ESTADO_CIVIL { get; set; } = string.Empty;
}

public class EstadosCivilesSearchDto
{
    public string? Search { get; set; }
}