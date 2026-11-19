using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_UNIDAD_MEDIDA - tipos equivalentes a la BD.</summary>
public class TiposUnidadMedida
{
    public string COD_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposUnidadMedidaInsertDto
{
    [Required] public string COD_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposUnidadMedidaUpdateDto
{
    [Required] public string COD_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposUnidadMedidaSearchDto
{
    public string? Search { get; set; }
}