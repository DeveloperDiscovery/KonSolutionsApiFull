using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad TIPOS_PROYECTO - tipos equivalentes a la BD.</summary>
public class TiposProyecto
{
    public string COD_TIPO_PROYECTO { get; set; } = string.Empty;
    public string DES_TIPO_PROYECTO { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PRESUPUESTO { get; set; }
    public bool? FLG_VALORADO { get; set; }
    public bool? FLG_SERVICIO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class TiposProyectoInsertDto
{
    [Required] public string COD_TIPO_PROYECTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PROYECTO { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PRESUPUESTO { get; set; }
    public bool? FLG_VALORADO { get; set; }
    public bool? FLG_SERVICIO { get; set; }
}

public class TiposProyectoUpdateDto
{
    [Required] public string COD_TIPO_PROYECTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PROYECTO { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PRESUPUESTO { get; set; }
    public bool? FLG_VALORADO { get; set; }
    public bool? FLG_SERVICIO { get; set; }
}

public class TiposProyectoSearchDto
{
    public string? Search { get; set; }
}
