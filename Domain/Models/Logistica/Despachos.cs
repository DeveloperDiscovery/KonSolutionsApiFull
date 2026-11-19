using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad DESPACHOS - tipos equivalentes a la BD.</summary>
public class Despachos
{
    public int? NUM_DESPACHO { get; set; }
    public string DES_DESPACHO { get; set; } = string.Empty;
    public DateTime? FEC_DESPACHO { get; set; }
    public decimal? COD_ENTIDAD_TRANSPORTISTA { get; set; }
    public string COD_TRANSPORTE { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
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
    public string DES_TRANSPORTE { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class DespachosInsertDto
{
    public int? NUM_DESPACHO { get; set; }
    [Required] public string DES_DESPACHO { get; set; } = string.Empty;
    public DateTime? FEC_DESPACHO { get; set; }
    public decimal? COD_ENTIDAD_TRANSPORTISTA { get; set; }
    [Required] public string COD_TRANSPORTE { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DespachosUpdateDto
{
    public int? NUM_DESPACHO { get; set; }
    [Required] public string DES_DESPACHO { get; set; } = string.Empty;
    public DateTime? FEC_DESPACHO { get; set; }
    public decimal? COD_ENTIDAD_TRANSPORTISTA { get; set; }
    [Required] public string COD_TRANSPORTE { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DespachosSearchDto
{
    public string? Search { get; set; }
}