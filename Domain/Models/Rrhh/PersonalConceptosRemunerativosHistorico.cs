using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CONCEPTOS_REMUNERATIVOS_HISTORICO - tipos equivalentes a la BD.</summary>
public class PersonalConceptosRemunerativosHistorico
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class PersonalConceptosRemunerativosHistoricoInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalConceptosRemunerativosHistoricoUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public int? COD_CONCEPTO_REMUNERATIVO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? VAL_CONCEPTO_REMUNERATIVO { get; set; }
    public decimal? TAS_CONCEPTO_REMUNERATIVO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalConceptosRemunerativosHistoricoSearchDto
{
    public string? Search { get; set; }
}