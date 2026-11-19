using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_TIPOS_ENTIDAD - tipos equivalentes a la BD.</summary>
public class ProyectosTiposEntidad
{
    public int? NUM_PROYECTO { get; set; }
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
}

public class ProyectosTiposEntidadInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosTiposEntidadUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosTiposEntidadSearchDto
{
    public string? Search { get; set; }
}