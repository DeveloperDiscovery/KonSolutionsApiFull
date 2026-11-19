using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad TIPOS_ORDEN_PRODUCCION - tipos equivalentes a la BD.</summary>
public class TiposOrdenProduccion
{
    public string COD_TIPO_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_TIPO_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public bool? FLG_ORDEN_PROPIA { get; set; }
    public bool? FLG_SERVICIO_PRODUCCION { get; set; }
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
}

public class TiposOrdenProduccionInsertDto
{
    [Required] public string COD_TIPO_ORDEN_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ORDEN_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public bool? FLG_ORDEN_PROPIA { get; set; }
    public bool? FLG_SERVICIO_PRODUCCION { get; set; }
}

public class TiposOrdenProduccionUpdateDto
{
    [Required] public string COD_TIPO_ORDEN_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ORDEN_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public bool? FLG_ORDEN_PROPIA { get; set; }
    public bool? FLG_SERVICIO_PRODUCCION { get; set; }
}

public class TiposOrdenProduccionSearchDto
{
    public string? Search { get; set; }
}