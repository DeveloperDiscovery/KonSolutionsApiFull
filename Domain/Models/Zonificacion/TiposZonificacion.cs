using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Zonificacion;

/// <summary>Entidad ZONIFICACION.TIPOS_ZONIFICACION — catálogo simple (piso, pasillo, sala,
/// depósito…) que clasifica las zonas. PK: COD_TIPO_ZONIFICACION.
/// Sin columnas de auditoría ni estado. IMG_ICONO es el ícono con que se dibuja la zona.</summary>
public class TiposZonificacion
{
    public string COD_TIPO_ZONIFICACION { get; set; } = string.Empty;
    public string? DES_TIPO_ZONIFICACION { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    /// <summary>Este tipo de zona se dibuja en el Mapa de Piso. Se marca en el catálogo y no
    /// se compara contra un código fijo: un código fijo se rompe el día que el catálogo se
    /// cargue en otra base con otra numeración.</summary>
    public bool? FLG_MAPEABLE { get; set; }
}

/// <summary>El SP _INSERT hace upsert por COD_TIPO_ZONIFICACION: el código lo escribe el
/// usuario (no hay correlativo automático) y es obligatorio en Crear y Modificar.</summary>
public class TiposZonificacionInsertDto
{
    [Required] public string COD_TIPO_ZONIFICACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_ZONIFICACION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public bool? FLG_MAPEABLE { get; set; }
}

public class TiposZonificacionSearchDto
{
    public string? Search { get; set; }
}
