using System.ComponentModel.DataAnnotations;

namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.ESTRUCTURA_CODEBAR - tipos equivalentes a la BD.</summary>
public class EstructuraCodebar
{
    public string COD_ESTRUCTURA_CODEBAR { get; set; } = string.Empty;
    public string? DES_ESTRUCTURA_CODEBAR { get; set; }
    public int? VAL_LONGITUD { get; set; }
    public int? NUM_ORDEN { get; set; }
}

public class EstructuraCodebarInsertDto
{
    [Required] public string COD_ESTRUCTURA_CODEBAR { get; set; } = string.Empty;
    public string? DES_ESTRUCTURA_CODEBAR { get; set; }
    public int? VAL_LONGITUD { get; set; }
    public int? NUM_ORDEN { get; set; }
}

public class EstructuraCodebarUpdateDto
{
    [Required] public string COD_ESTRUCTURA_CODEBAR { get; set; } = string.Empty;
    public string? DES_ESTRUCTURA_CODEBAR { get; set; }
    public int? VAL_LONGITUD { get; set; }
    public int? NUM_ORDEN { get; set; }
}
