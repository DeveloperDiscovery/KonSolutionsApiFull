using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_IMPUESTO - tipos equivalentes a la BD.</summary>
public class TiposImpuesto
{
    public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    public string DES_TIPO_IMPUESTO { get; set; } = string.Empty;
    public int? COD_TIPO_IMPUESTO_SUNAT { get; set; }
}

public class TiposImpuestoInsertDto
{
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_IMPUESTO { get; set; } = string.Empty;
    [Required] public int? COD_TIPO_IMPUESTO_SUNAT { get; set; }
}

public class TiposImpuestoUpdateDto
{
    [Required] public string COD_TIPO_IMPUESTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_IMPUESTO { get; set; } = string.Empty;
    [Required] public int? COD_TIPO_IMPUESTO_SUNAT { get; set; }
}

public class TiposImpuestoSearchDto
{
    public string? Search { get; set; }
}