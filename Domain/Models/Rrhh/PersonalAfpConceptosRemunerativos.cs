using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_AFP_CONCEPTOS_REMUNERATIVOS - tipos equivalentes a la BD.</summary>
public class PersonalAfpConceptosRemunerativos
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_CUENTA_CONTABLE_DEBE { get; set; } = string.Empty;
    public string COD_CUENTA_CONTABLE_HABER { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
    public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
}

public class PersonalAfpConceptosRemunerativosInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_CUENTA_CONTABLE_DEBE { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE_HABER { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalAfpConceptosRemunerativosUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    [Required] public string COD_CUENTA_CONTABLE_DEBE { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE_HABER { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalAfpConceptosRemunerativosSearchDto
{
    public string? Search { get; set; }
}