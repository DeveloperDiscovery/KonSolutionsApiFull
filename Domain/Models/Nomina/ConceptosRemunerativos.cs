using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad CONCEPTOS_REMUNERATIVOS - tipos equivalentes a la BD.</summary>
public class ConceptosRemunerativos
{
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    public bool? FLG_TIEMPO { get; set; }
    public bool? FLG_FIJO { get; set; }
    public bool? FLG_TASA { get; set; }
    public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_REMUNERATIVO { get; set; } = string.Empty;
}

public class ConceptosRemunerativosInsertDto
{
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    [Required] public string DES_ALIAS { get; set; } = string.Empty;
    [Required] public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    public bool? FLG_TIEMPO { get; set; }
    public bool? FLG_FIJO { get; set; }
    public bool? FLG_TASA { get; set; }
    [Required] public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConceptosRemunerativosUpdateDto
{
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    [Required] public string DES_ALIAS { get; set; } = string.Empty;
    [Required] public string COD_TIPO_REMUNERATIVO { get; set; } = string.Empty;
    public bool? FLG_TIEMPO { get; set; }
    public bool? FLG_FIJO { get; set; }
    public bool? FLG_TASA { get; set; }
    [Required] public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConceptosRemunerativosSearchDto
{
    public string? Search { get; set; }
}