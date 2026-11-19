using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad TIPOS_AUTORIZACION - tipos equivalentes a la BD.</summary>
public class TiposAutorizacion
{
    public string COD_TIPO_AUTORIZACION { get; set; } = string.Empty;
    public string DES_TIPO_AUTORIZACION { get; set; } = string.Empty;
}

public class TiposAutorizacionInsertDto
{
    [Required] public string COD_TIPO_AUTORIZACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_AUTORIZACION { get; set; } = string.Empty;
}

public class TiposAutorizacionUpdateDto
{
    [Required] public string COD_TIPO_AUTORIZACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_AUTORIZACION { get; set; } = string.Empty;
}

public class TiposAutorizacionSearchDto
{
    public string? Search { get; set; }
}