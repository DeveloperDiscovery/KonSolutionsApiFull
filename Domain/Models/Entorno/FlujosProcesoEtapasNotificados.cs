using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad FLUJOS_PROCESO_ETAPAS_NOTIFICADOS - tipos equivalentes a la BD.</summary>
public class FlujosProcesoEtapasNotificados
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_ETAPA { get; set; }
    public decimal? COD_ENTIDAD_NOTIFICAR { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_ETAPA { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class FlujosProcesoEtapasNotificadosInsertDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_ETAPA { get; set; }
    public decimal? COD_ENTIDAD_NOTIFICAR { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoEtapasNotificadosUpdateDto
{
    public int? NUM_FLUJO_PROCESO { get; set; }
    public int? NUM_ETAPA { get; set; }
    public decimal? COD_ENTIDAD_NOTIFICAR { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FlujosProcesoEtapasNotificadosSearchDto
{
    public string? Search { get; set; }
}