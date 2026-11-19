using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad TIPOS_PLANILLA - tipos equivalentes a la BD.</summary>
public class TiposPlanilla
{
    public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public string DES_TIPO_PLANILLA { get; set; } = string.Empty;
    public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    public string DES_TIPO_PERSONAL { get; set; } = string.Empty;
}

public class TiposPlanillaInsertDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PLANILLA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPlanillaUpdateDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PLANILLA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPlanillaSearchDto
{
    public string? Search { get; set; }
}