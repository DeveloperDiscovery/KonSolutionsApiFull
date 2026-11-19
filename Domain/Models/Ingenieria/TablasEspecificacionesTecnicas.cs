using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad TABLAS_ESPECIFICACIONES_TECNICAS - tipos equivalentes a la BD.</summary>
public class TablasEspecificacionesTecnicas
{
    public string COD_TABLA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_FOREING_KEY { get; set; } = string.Empty;
    public string DES_SET { get; set; } = string.Empty;
    public bool? FLG_HABILITADA { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class TablasEspecificacionesTecnicasInsertDto
{
    [Required] public string COD_TABLA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_FOREING_KEY { get; set; } = string.Empty;
    [Required] public string DES_SET { get; set; } = string.Empty;
    public bool? FLG_HABILITADA { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TablasEspecificacionesTecnicasUpdateDto
{
    [Required] public string COD_TABLA_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    [Required] public string DES_FOREING_KEY { get; set; } = string.Empty;
    [Required] public string DES_SET { get; set; } = string.Empty;
    public bool? FLG_HABILITADA { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TablasEspecificacionesTecnicasSearchDto
{
    public string? Search { get; set; }
}