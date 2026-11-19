using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad TIPOS_CENTRO_COSTO - tipos equivalentes a la BD.</summary>
public class TiposCentroCosto
{
    public string COD_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
}

public class TiposCentroCostoInsertDto
{
    [Required] public string COD_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
}

public class TiposCentroCostoUpdateDto
{
    [Required] public string COD_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
}

public class TiposCentroCostoSearchDto
{
    public string? Search { get; set; }
}