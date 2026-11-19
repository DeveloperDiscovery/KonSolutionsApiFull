using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad PERIODOS - tipos equivalentes a la BD.</summary>
public class Periodos
{
    public int? NUM_PERIODO { get; set; }
    public string COD_PERIODO { get; set; } = string.Empty;
    public string DES_PERIODO { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? NUM_ANO { get; set; }
    public int? NUM_SEMESTRE { get; set; }
    public int? NUM_QUARTER { get; set; }
    public int? NUM_MES { get; set; }
    public int? NUM_SEMANA { get; set; }
    public int? NUM_DIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_COMENTARIO_CIERRE { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    public string NUM_PERIODO_CONTABLE { get; set; } = string.Empty;
}

public class PeriodosInsertDto
{
    public int? NUM_PERIODO { get; set; }
    [Required] public string COD_PERIODO { get; set; } = string.Empty;
    [Required] public string DES_PERIODO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? NUM_ANO { get; set; }
    public int? NUM_SEMESTRE { get; set; }
    public int? NUM_QUARTER { get; set; }
    public int? NUM_MES { get; set; }
    public int? NUM_SEMANA { get; set; }
    public int? NUM_DIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PeriodosUpdateDto
{
    public int? NUM_PERIODO { get; set; }
    [Required] public string COD_PERIODO { get; set; } = string.Empty;
    [Required] public string DES_PERIODO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? NUM_ANO { get; set; }
    public int? NUM_SEMESTRE { get; set; }
    public int? NUM_QUARTER { get; set; }
    public int? NUM_MES { get; set; }
    public int? NUM_SEMANA { get; set; }
    public int? NUM_DIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PeriodosSearchDto
{
    public string CodTipoPeriodo { get; set; } = string.Empty;
    public string? Search { get; set; }
}