using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CONCEPTOS_REMUNERATIVOS - tipos equivalentes a la BD.</summary>
public class PersonalConceptosRemunerativos
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_CONCEPTO_REMUNERATIVO { get; set; } = string.Empty;
}

public class PersonalConceptosRemunerativosInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalConceptosRemunerativosUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalConceptosRemunerativosSearchDto
{
    public string? Search { get; set; }
}