using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad AUTORIZACIONES_EVENTOS - tipos equivalentes a la BD.</summary>
public class AutorizacionesEventos
{
    public int? NUM_AUTORIZACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_USUARIO { get; set; } = string.Empty;
    public string DES_EVENTO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
}

public class AutorizacionesEventosInsertDto
{
    public int? NUM_AUTORIZACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_EVENTO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AutorizacionesEventosUpdateDto
{
    public int? NUM_AUTORIZACION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_EVENTO { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AutorizacionesEventosSearchDto
{
    public string? Search { get; set; }
}