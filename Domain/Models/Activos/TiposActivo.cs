using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Activos;

/// <summary>Entidad TIPOS_ACTIVO - tipos equivalentes a la BD.</summary>
public class TiposActivo
{
    public string COD_TIPO_ACTIVO { get; set; } = string.Empty;
    public string DES_TIPO_ACTIVO { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
}

public class TiposActivoInsertDto
{
    [Required] public string COD_TIPO_ACTIVO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ACTIVO { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
}

public class TiposActivoUpdateDto
{
    [Required] public string COD_TIPO_ACTIVO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ACTIVO { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
}

public class TiposActivoSearchDto
{
    public string? Search { get; set; }
}