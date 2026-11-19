using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad GRUPOS_PLANEAMIENTO - tipos equivalentes a la BD.</summary>
public class GruposPlaneamiento
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public string DES_GRUPO_PLANEAMIENTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_CLIENTE { get; set; }
    public DateTime? FEC_DESPACHO_INICIO { get; set; }
    public DateTime? FEC_DESPACHO_FINAL { get; set; }
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
    public decimal? COD_ENTIDAD { get; set; }
}

public class GruposPlaneamientoInsertDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    [Required] public string DES_GRUPO_PLANEAMIENTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_CLIENTE { get; set; }
    public DateTime? FEC_DESPACHO_INICIO { get; set; }
    public DateTime? FEC_DESPACHO_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposPlaneamientoUpdateDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    [Required] public string DES_GRUPO_PLANEAMIENTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_CLIENTE { get; set; }
    public DateTime? FEC_DESPACHO_INICIO { get; set; }
    public DateTime? FEC_DESPACHO_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposPlaneamientoSearchDto
{
    public string? Search { get; set; }
}