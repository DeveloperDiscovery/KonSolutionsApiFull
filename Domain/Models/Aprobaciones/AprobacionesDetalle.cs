using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Aprobaciones;

/// <summary>Entidad APROBACIONES_DETALLE - tipos equivalentes a la BD.</summary>
public class AprobacionesDetalle
{
    public int? NUM_APROBACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public DateTime? FEC_EVALUACION_MAXIMA { get; set; }
    public DateTime? FEC_EVALUACION_EFECTUADA { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_APROBACION { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
}

public class AprobacionesDetalleInsertDto
{
    public int? NUM_APROBACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public DateTime? FEC_EVALUACION_MAXIMA { get; set; }
    public DateTime? FEC_EVALUACION_EFECTUADA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AprobacionesDetalleUpdateDto
{
    public int? NUM_APROBACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public DateTime? FEC_EVALUACION_MAXIMA { get; set; }
    public DateTime? FEC_EVALUACION_EFECTUADA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AprobacionesDetalleSearchDto
{
    public string? Search { get; set; }
}