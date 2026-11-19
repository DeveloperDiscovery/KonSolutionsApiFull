using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad PLANEAMIENTO_CONFIGURACION - tipos equivalentes a la BD.</summary>
public class PlaneamientoConfiguracion
{
    public int? NUM_SECUENCIA { get; set; }
    public string COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD { get; set; } = string.Empty;
    public string DES_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_SINGULAR { get; set; } = string.Empty;
    public string DES_SUFIJO { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class PlaneamientoConfiguracionInsertDto
{
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD { get; set; } = string.Empty;
}

public class PlaneamientoConfiguracionUpdateDto
{
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD { get; set; } = string.Empty;
}

public class PlaneamientoConfiguracionSearchDto
{
    public string? Search { get; set; }
}