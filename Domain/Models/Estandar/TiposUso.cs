using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.TIPOS_USO. Tabla mínima: solo código (nvarchar(3)) y
/// descripción (nvarchar(25)); no lleva estado ni auditoría.</summary>
public class TiposUso
{
    public string COD_TIPO_USO { get; set; } = string.Empty;
    public string DES_TIPO_USO { get; set; } = string.Empty;
}

/// <summary>DTO de guardado. PROC_ESTANDAR_TIPOS_USO_INSERT es un upsert
/// (intenta UPDATE y, si no afecta filas, hace INSERT) — siempre se llama por POST.</summary>
public class TiposUsoInsertDto
{
    [Required] public string COD_TIPO_USO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_USO { get; set; } = string.Empty;
}
