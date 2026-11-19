using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_LOTE_CONTROL - tipos equivalentes a la BD.</summary>
public class TiposLoteControl
{
    public string COD_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    public bool? FLG_LOCAL { get; set; }
}

public class TiposLoteControlInsertDto
{
    [Required] public string COD_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    public bool? FLG_LOCAL { get; set; }
}

public class TiposLoteControlUpdateDto
{
    [Required] public string COD_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_LOTE_CONTROL { get; set; } = string.Empty;
    public bool? FLG_LOCAL { get; set; }
}

public class TiposLoteControlSearchDto
{
    public string? Search { get; set; }
}