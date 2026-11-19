using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Soporte;

/// <summary>Entidad TICKETS_ATENCION_DETALLE - tipos equivalentes a la BD.</summary>
public class TicketsAtencionDetalle
{
    public int? NUM_TICKET { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_AREA_EJECUTORA { get; set; }
    public int? COD_CARGO_EJECUTORA { get; set; }
    public decimal? COD_ENTIDAD_EJECUTORA { get; set; }
    public DateTime? FEC_ATENCION_PROGRAMADA { get; set; }
    public DateTime? FEC_ATENCION_EJECUTADA { get; set; }
    public string DES_DETALLADA { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TAREA { get; set; } = string.Empty;
    public string DES_TICKET { get; set; } = string.Empty;
    public decimal? COD_USUARIO_EJECUTORA { get; set; }
}

public class TicketsAtencionDetalleInsertDto
{
    public int? NUM_TICKET { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_AREA_EJECUTORA { get; set; }
    public int? COD_CARGO_EJECUTORA { get; set; }
    public decimal? COD_ENTIDAD_EJECUTORA { get; set; }
    public DateTime? FEC_ATENCION_PROGRAMADA { get; set; }
    public DateTime? FEC_ATENCION_EJECUTADA { get; set; }
    [Required] public string DES_DETALLADA { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_USUARIO_EJECUTORA { get; set; }
}

public class TicketsAtencionDetalleUpdateDto
{
    public int? NUM_TICKET { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_TAREA { get; set; }
    public int? COD_AREA_EJECUTORA { get; set; }
    public int? COD_CARGO_EJECUTORA { get; set; }
    public decimal? COD_ENTIDAD_EJECUTORA { get; set; }
    public DateTime? FEC_ATENCION_PROGRAMADA { get; set; }
    public DateTime? FEC_ATENCION_EJECUTADA { get; set; }
    [Required] public string DES_DETALLADA { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_USUARIO_EJECUTORA { get; set; }
}

public class TicketsAtencionDetalleSearchDto
{
    public string? Search { get; set; }
}