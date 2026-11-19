using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.TIPOS_UBICACION — catálogo que clasifica las ubicaciones de
/// un almacén (casillero de rack, zona de piso, playa de descarga…).
/// PK: COD_TIPO_UBICACION. Sin auditoría ni estado.</summary>
public class TiposUbicacion
{
    public string COD_TIPO_UBICACION { get; set; } = string.Empty;
    public string? DES_TIPO_UBICACION { get; set; }
    /// <summary>Imagen con que se representa el tipo de ubicación (columna IMG_IMAGEN).</summary>
    public byte[]? IMG_IMAGEN { get; set; }
}

/// <summary>El SP _INSERT hace upsert por COD_TIPO_UBICACION: el código lo escribe el
/// usuario (no hay correlativo automático) y es obligatorio en Crear y Modificar.</summary>
public class TiposUbicacionInsertDto
{
    [Required] public string COD_TIPO_UBICACION { get; set; } = string.Empty;
    [Required] public string DES_TIPO_UBICACION { get; set; } = string.Empty;
    public byte[]? IMG_IMAGEN { get; set; }
}
