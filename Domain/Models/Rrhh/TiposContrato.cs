using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_CONTRATO - tipos equivalentes a la BD.</summary>
public class TiposContrato
{
    public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public string DES_TIPO_CONTRATO { get; set; } = string.Empty;
    public bool? FLG_INDEFINIDO { get; set; }
    public string DES_SUB_TIPO { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_DURACION_MAXIMA { get; set; }
    public bool? FLG_CAUSA_OBJETIVA { get; set; }
    public string DES_USO_CONTRACTUAL { get; set; } = string.Empty;
}

public class TiposContratoInsertDto
{
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CONTRATO { get; set; } = string.Empty;
    public bool? FLG_INDEFINIDO { get; set; }
    [Required] public string DES_SUB_TIPO { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_DURACION_MAXIMA { get; set; }
    public bool? FLG_CAUSA_OBJETIVA { get; set; }
    [Required] public string DES_USO_CONTRACTUAL { get; set; } = string.Empty;
}

public class TiposContratoUpdateDto
{
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CONTRATO { get; set; } = string.Empty;
    public bool? FLG_INDEFINIDO { get; set; }
    [Required] public string DES_SUB_TIPO { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA_TIEMPO { get; set; } = string.Empty;
    public int? CAN_DURACION_MAXIMA { get; set; }
    public bool? FLG_CAUSA_OBJETIVA { get; set; }
    [Required] public string DES_USO_CONTRACTUAL { get; set; } = string.Empty;
}

public class TiposContratoSearchDto
{
    public string? Search { get; set; }
}