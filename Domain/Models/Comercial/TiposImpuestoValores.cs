using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_IMPUESTO_VALORES - tipos equivalentes a la BD.</summary>
public class TiposImpuestoValores
{
    public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_IMPUESTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_IMPUESTO { get; set; } = string.Empty;
}

public class TiposImpuestoValoresInsertDto
{
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_IMPUESTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposImpuestoValoresUpdateDto
{
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public decimal? TAS_IMPUESTO { get; set; }
    public decimal? VAL_IMPUESTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposImpuestoValoresSearchDto
{
    public string? Search { get; set; }
}