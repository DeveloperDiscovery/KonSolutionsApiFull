using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad INGENIERIA.TIPOS_DESARROLLO — tipos equivalentes a la BD.
/// Clave compuesta (COD_CLASE_DESARROLLO + COD_TIPO_DESARROLLO). DES_CLASE_DESARROLLO
/// es solo lectura: viene del LEFT JOIN contra INGENIERIA.CLASES_DESARROLLO.</summary>
public class TiposDesarrollo
{
    public string COD_CLASE_DESARROLLO { get; set; } = string.Empty;
    public string COD_TIPO_DESARROLLO  { get; set; } = string.Empty;
    public string DES_TIPO_DESARROLLO  { get; set; } = string.Empty;

    // Solo lectura (JOIN).
    public string DES_CLASE_DESARROLLO { get; set; } = string.Empty;
}

/// <summary>DTO de guardado. PROC_INGENIERIA_TIPOS_DESARROLLO_INSERT es un upsert
/// (intenta UPDATE y, si no afecta filas, hace INSERT) — siempre se llama por POST.</summary>
public class TiposDesarrolloInsertDto
{
    [Required] public string COD_CLASE_DESARROLLO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DESARROLLO  { get; set; } = string.Empty;
    [Required] public string DES_TIPO_DESARROLLO  { get; set; } = string.Empty;
}

public class TiposDesarrolloSearchDto
{
    public string? Search { get; set; }
}
