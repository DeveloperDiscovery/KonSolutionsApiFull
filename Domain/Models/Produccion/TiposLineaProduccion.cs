using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad TIPOS_LINEA_PRODUCCION - tipos equivalentes a la BD.</summary>
public class TiposLineaProduccion
{
    public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PLANTA_PROPIA { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposLineaProduccionInsertDto
{
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PLANTA_PROPIA { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposLineaProduccionUpdateDto
{
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public bool? FLG_PRODUCCION { get; set; }
    public bool? FLG_PLANTA_PROPIA { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
}

public class TiposLineaProduccionSearchDto
{
    public string? Search { get; set; }
}