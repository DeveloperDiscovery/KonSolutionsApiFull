using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Soporte;

/// <summary>Entidad TICKETS_ATENCION_DETALLE_PASOS - tipos equivalentes a la BD.</summary>
public class TicketsAtencionDetallePasos
{
    public int? NUM_TICKET { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PASO { get; set; }
    public string DES_PASO { get; set; } = string.Empty;
    public string DES_ANOTACIONES { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
}

public class TicketsAtencionDetallePasosInsertDto
{
    public int? NUM_TICKET { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PASO { get; set; }
    [Required] public string DES_PASO { get; set; } = string.Empty;
    [Required] public string DES_ANOTACIONES { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TicketsAtencionDetallePasosUpdateDto
{
    public int? NUM_TICKET { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PASO { get; set; }
    [Required] public string DES_PASO { get; set; } = string.Empty;
    [Required] public string DES_ANOTACIONES { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TicketsAtencionDetallePasosSearchDto
{
    public string? Search { get; set; }
}