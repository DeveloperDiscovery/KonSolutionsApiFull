using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad TIPOS_PLANILLA_CONCEPTOS_REMUNERATIVOS - tipos equivalentes a la BD.</summary>
public class TiposPlanillaConceptosRemunerativos
{
    public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_CUENTA_CONTABLE_DEBE { get; set; } = string.Empty;
    public string COD_CUENTA_CONTABLE_HABER { get; set; } = string.Empty;
    public string DES_FORMULA { get; set; } = string.Empty;
    public string DES_FORMULA_MASCARA { get; set; } = string.Empty;
    public int? NUM_ORDEN_CALCULO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string DES_TIPO_PLANILLA { get; set; } = string.Empty;
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
}

public class TiposPlanillaConceptosRemunerativosInsertDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_CUENTA_CONTABLE_DEBE { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE_HABER { get; set; } = string.Empty;
    [Required] public string DES_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_FORMULA_MASCARA { get; set; } = string.Empty;
    public int? NUM_ORDEN_CALCULO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPlanillaConceptosRemunerativosUpdateDto
{
    [Required] public string COD_TIPO_PLANILLA { get; set; } = string.Empty;
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_CUENTA_CONTABLE_DEBE { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE_HABER { get; set; } = string.Empty;
    [Required] public string DES_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_FORMULA_MASCARA { get; set; } = string.Empty;
    public int? NUM_ORDEN_CALCULO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposPlanillaConceptosRemunerativosSearchDto
{
    public string? Search { get; set; }
}