using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad TIPOS_CATEGORIA_COMISION - tipos equivalentes a la BD.</summary>
public class TiposCategoriaComision
{
    public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    public string DES_TIPO_COMISION { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposCategoriaComisionInsertDto
{
    [Required] public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COMISION { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposCategoriaComisionUpdateDto
{
    [Required] public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COMISION { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposCategoriaComisionSearchDto
{
    public string? Search { get; set; }
}