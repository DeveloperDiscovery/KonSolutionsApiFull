using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad TIPOS_NOTIFICACION_ACCIONES - tipos equivalentes a la BD.</summary>
public class TiposNotificacionAcciones
{
    public string COD_TIPO_NOTIFICACION { get; set; } = string.Empty;
    public int NUM_SECUENCIA { get; set; }
    public string? DES_ACCION { get; set; }
    public bool? FLG_LECTURA { get; set; }
    public bool? FLG_APROBAR { get; set; }
    public bool? FLG_RECHAZO { get; set; }
    public bool? FLG_OBSERVAR { get; set; }
}

public class TiposNotificacionAccionesInsertDto
{
    [Required] public string COD_TIPO_NOTIFICACION { get; set; } = string.Empty;
    // 0 al crear: el propio SP INSERT calcula la siguiente secuencia (MAX+1) cuando el
    // UPDATE inicial no afecta ninguna fila — igual que el correlativo de otras tablas.
    public int NUM_SECUENCIA { get; set; }
    [Required] public string DES_ACCION { get; set; } = string.Empty;
    public bool? FLG_LECTURA { get; set; }
    public bool? FLG_APROBAR { get; set; }
    public bool? FLG_RECHAZO { get; set; }
    public bool? FLG_OBSERVAR { get; set; }
}

public class TiposNotificacionAccionesSearchDto
{
    [Required] public string COD_TIPO_NOTIFICACION { get; set; } = string.Empty;
    public string? Search { get; set; }
}
