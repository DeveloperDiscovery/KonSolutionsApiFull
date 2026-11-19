using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Activos;

/// <summary>Entidad ACTIVOS_ENTIDADES - tipos equivalentes a la BD.</summary>
public class ActivosEntidades
{
    public string COD_ACTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public DateTime? FEC_ASIGNACION_INICIO { get; set; }
    public DateTime? FEC_ASIGNACION_FINAL { get; set; }
    public bool? FLG_PORTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ACTIVO { get; set; } = string.Empty;
    public string DES_MARCA { get; set; } = string.Empty;
    public string DES_MODELO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_SERIE { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class ActivosEntidadesInsertDto
{
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public DateTime? FEC_ASIGNACION_INICIO { get; set; }
    public DateTime? FEC_ASIGNACION_FINAL { get; set; }
    public bool? FLG_PORTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosEntidadesUpdateDto
{
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public DateTime? FEC_ASIGNACION_INICIO { get; set; }
    public DateTime? FEC_ASIGNACION_FINAL { get; set; }
    public bool? FLG_PORTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosEntidadesSearchDto
{
    public string? Search { get; set; }
}