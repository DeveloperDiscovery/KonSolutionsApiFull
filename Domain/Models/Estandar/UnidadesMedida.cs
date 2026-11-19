using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad UNIDADES_MEDIDA - tipos equivalentes a la BD.</summary>
public class UnidadesMedida
{
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA_SUNAT { get; set; } = string.Empty;
    public string COD_SIMBOLO { get; set; } = string.Empty;
    public string COD_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public string DES_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
}

public class UnidadesMedidaInsertDto
{
    [Required] public string  COD_UNIDAD_MEDIDA       { get; set; } = string.Empty;
    [Required] public string  DES_UNIDAD_MEDIDA       { get; set; } = string.Empty;
    public string?            COD_UNIDAD_MEDIDA_SUNAT { get; set; }
    public string?            COD_SIMBOLO             { get; set; }
    [Required] public string  COD_TIPO_UNIDAD_MEDIDA  { get; set; } = string.Empty;
    public byte[]?            IMG_ICONO               { get; set; }
}

public class UnidadesMedidaUpdateDto
{
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    [Required] public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string? COD_UNIDAD_MEDIDA_SUNAT { get; set; }
    public string? COD_SIMBOLO { get; set; }
    [Required] public string COD_TIPO_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class UnidadesMedidaSearchDto
{
    public string? Search { get; set; }
}