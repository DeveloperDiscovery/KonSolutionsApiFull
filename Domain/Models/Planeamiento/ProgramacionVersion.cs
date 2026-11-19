using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad PROGRAMACION_VERSION - tipos equivalentes a la BD.</summary>
public class ProgramacionVersion
{
    public int? NUM_PROGRAMA { get; set; }
    public int? NUM_VERSION { get; set; }
    public string DES_VERSION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? TAS_EFICIENCIA_TIEMPO { get; set; }
    public decimal? TAS_EFICIENCIA_CANTIDAD { get; set; }
    public DateTime? FEC_PROGRAMADA_INICIO { get; set; }
    public DateTime? FEC_PROGRAMADA_FINAL { get; set; }
    public DateTime? FEC_REAL_INICIO { get; set; }
    public DateTime? FEC_REAL_FINAL { get; set; }
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
    public string DES_PROGRAMA { get; set; } = string.Empty;
}

public class ProgramacionVersionInsertDto
{
    public int? NUM_PROGRAMA { get; set; }
    public int? NUM_VERSION { get; set; }
    [Required] public string DES_VERSION { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? TAS_EFICIENCIA_TIEMPO { get; set; }
    public decimal? TAS_EFICIENCIA_CANTIDAD { get; set; }
    public DateTime? FEC_PROGRAMADA_INICIO { get; set; }
    public DateTime? FEC_PROGRAMADA_FINAL { get; set; }
    public DateTime? FEC_REAL_INICIO { get; set; }
    public DateTime? FEC_REAL_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionVersionUpdateDto
{
    public int? NUM_PROGRAMA { get; set; }
    public int? NUM_VERSION { get; set; }
    [Required] public string DES_VERSION { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public decimal? TAS_EFICIENCIA_TIEMPO { get; set; }
    public decimal? TAS_EFICIENCIA_CANTIDAD { get; set; }
    public DateTime? FEC_PROGRAMADA_INICIO { get; set; }
    public DateTime? FEC_PROGRAMADA_FINAL { get; set; }
    public DateTime? FEC_REAL_INICIO { get; set; }
    public DateTime? FEC_REAL_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionVersionSearchDto
{
    public string? Search { get; set; }
}