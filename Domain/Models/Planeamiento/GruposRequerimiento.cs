using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad GRUPOS_REQUERIMIENTO - tipos equivalentes a la BD.</summary>
public class GruposRequerimiento
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? COD_GRUPO_REQUERIMIENTO { get; set; }
    public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CLASE_PROCESO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_GRUPO_PLANEAMIENTO { get; set; } = string.Empty;
}

public class GruposRequerimientoInsertDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? COD_GRUPO_REQUERIMIENTO { get; set; }
    [Required] public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposRequerimientoUpdateDto
{
    public int? COD_GRUPO_PLANEAMIENTO { get; set; }
    public int? COD_GRUPO_REQUERIMIENTO { get; set; }
    [Required] public string COD_CLASE_PROCESO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposRequerimientoSearchDto
{
    public string? Search { get; set; }
}