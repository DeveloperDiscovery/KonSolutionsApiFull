using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Soporte;

/// <summary>Entidad TICKETS_ATENCION - tipos equivalentes a la BD.</summary>
public class TicketsAtencion
{
    public int? NUM_TICKET { get; set; }
    public string DES_TICKET { get; set; } = string.Empty;
    public int? COD_AREA_SOLICITANTE { get; set; }
    public int? COD_CARGO_SOLICITANTE { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    public string DES_DETALLADA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public decimal? COD_USUARIO_SOLICITANTE { get; set; }
}

public class TicketsAtencionInsertDto
{
    public int? NUM_TICKET { get; set; }
    [Required] public string DES_TICKET { get; set; } = string.Empty;
    public int? COD_AREA_SOLICITANTE { get; set; }
    public int? COD_CARGO_SOLICITANTE { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    [Required] public string DES_DETALLADA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_USUARIO_SOLICITANTE { get; set; }
}

public class TicketsAtencionUpdateDto
{
    public int? NUM_TICKET { get; set; }
    [Required] public string DES_TICKET { get; set; } = string.Empty;
    public int? COD_AREA_SOLICITANTE { get; set; }
    public int? COD_CARGO_SOLICITANTE { get; set; }
    public decimal? COD_ENTIDAD_SOLICITANTE { get; set; }
    public DateTime? FEC_SOLICITUD { get; set; }
    [Required] public string DES_DETALLADA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_USUARIO_SOLICITANTE { get; set; }
}

public class TicketsAtencionSearchDto
{
    public string? Search { get; set; }
}