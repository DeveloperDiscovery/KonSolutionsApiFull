using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad INGENIERIA.CLASES_DESARROLLO — tipos equivalentes a la BD.
/// Tabla mínima: solo código (nvarchar(3)) y descripción (nvarchar(50)); no lleva
/// estado ni auditoría, por eso el modelo no replica esos campos.</summary>
public class ClasesDesarrollo
{
    public string COD_CLASE_DESARROLLO { get; set; } = string.Empty;
    public string DES_CLASE_DESARROLLO { get; set; } = string.Empty;
}

/// <summary>DTO de guardado. PROC_INGENIERIA_CLASES_DESARROLLO_INSERT es un upsert
/// (intenta UPDATE y, si no afecta filas, hace INSERT) — siempre se llama por POST.</summary>
public class ClasesDesarrolloInsertDto
{
    [Required] public string COD_CLASE_DESARROLLO { get; set; } = string.Empty;
    [Required] public string DES_CLASE_DESARROLLO { get; set; } = string.Empty;
}
