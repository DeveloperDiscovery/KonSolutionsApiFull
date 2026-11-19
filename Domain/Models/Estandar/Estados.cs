using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTADOS - tipos equivalentes a la BD.</summary>
public class Estados
{
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string DES_ESTADO { get; set; } = string.Empty;
    public string? COD_ESTADO_ALIAS { get; set; }
    public bool? FLG_DEFAULT { get; set; }
    public bool? FLG_DISPARADOR_MAIL { get; set; }
    public bool? FLG_DISPARADOR_NOTIFICACION { get; set; }
    public bool? FLG_DISPARADOR_AUTORIZACION { get; set; }
    public bool? FLG_DISPARADOR_AUTORIZACION_COMERCIAL { get; set; }

    // Marca el estado como el disparador de un proceso de reversa (ej. revertir
    // reservas/requerimiento de compra) — ver PROC_LOGISTICA_REQUERIMIENTOS_STOCK_REVERSERESERVA.
    public bool? FLG_PROCESO_REVERSA { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_ESTADO { get; set; } = string.Empty;
    public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
}

public class EstadosInsertDto
{
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public bool EsNuevo { get; set; }
    [Required] public string DES_ESTADO { get; set; } = string.Empty;
    public string? COD_ESTADO_ALIAS { get; set; }
    public bool? FLG_DEFAULT { get; set; }
    public bool? FLG_DISPARADOR_MAIL { get; set; }
    public bool? FLG_DISPARADOR_NOTIFICACION { get; set; }
    public bool? FLG_DISPARADOR_AUTORIZACION { get; set; }
    public bool? FLG_DISPARADOR_AUTORIZACION_COMERCIAL { get; set; }
    public bool? FLG_PROCESO_REVERSA { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    [Required] public string DES_BACKCOLOR { get; set; } = string.Empty;
    [Required] public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EstadosUpdateDto
{
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    [Required] public string DES_ESTADO { get; set; } = string.Empty;
    public string? COD_ESTADO_ALIAS { get; set; }
    public bool? FLG_DEFAULT { get; set; }
    public bool? FLG_DISPARADOR_MAIL { get; set; }
    public bool? FLG_DISPARADOR_NOTIFICACION { get; set; }
    public bool? FLG_DISPARADOR_AUTORIZACION { get; set; }
    public bool? FLG_DISPARADOR_AUTORIZACION_COMERCIAL { get; set; }
    public bool? FLG_PROCESO_REVERSA { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    [Required] public string DES_BACKCOLOR { get; set; } = string.Empty;
    [Required] public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EstadosSearchDto
{
    public string? Search { get; set; }
}