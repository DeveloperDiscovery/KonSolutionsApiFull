using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad TIPOS_PROGRAMACION - tipos equivalentes a la BD.</summary>
public class TiposProgramacion
{
    public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public string DES_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_TIEMPO_MUERTO { get; set; }
}

public class TiposProgramacionInsertDto
{
    [Required] public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_TIEMPO_MUERTO { get; set; }
}

public class TiposProgramacionUpdateDto
{
    [Required] public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_TIEMPO_MUERTO { get; set; }
}

public class TiposProgramacionSearchDto
{
    public string? Search { get; set; }
}