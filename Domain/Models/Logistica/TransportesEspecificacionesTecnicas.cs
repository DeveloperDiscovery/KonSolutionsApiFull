using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TRANSPORTES_ESPECIFICACIONES_TECNICAS - tipos equivalentes a la BD.</summary>
public class TransportesEspecificacionesTecnicas
{
    public string COD_TRANSPORTE { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string VAL_ESPECIFICACION_TENICA { get; set; } = string.Empty;
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ITEM_TECNICO { get; set; } = string.Empty;
    public string DES_ITEM_TECNICO_SINGULAR { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TRANSPORTE { get; set; } = string.Empty;
}

public class TransportesEspecificacionesTecnicasInsertDto
{
    [Required] public string COD_TRANSPORTE { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    [Required] public string VAL_ESPECIFICACION_TENICA { get; set; } = string.Empty;
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TransportesEspecificacionesTecnicasUpdateDto
{
    [Required] public string COD_TRANSPORTE { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    [Required] public string VAL_ESPECIFICACION_TENICA { get; set; } = string.Empty;
    public decimal? VAL_MINIMO { get; set; }
    public decimal? VAL_MAXIMO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TransportesEspecificacionesTecnicasSearchDto
{
    public string COD_TRANSPORTE { get; set; } = string.Empty;
    public string? Search { get; set; }
}