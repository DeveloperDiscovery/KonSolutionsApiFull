using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad ENTORNO.DICCIONARIO_ETIQUETAS - catálogo simple, COD_ETIQUETA lo ingresa
/// el usuario (no es identity).</summary>
public class DiccionarioEtiquetas
{
    public string COD_ETIQUETA { get; set; } = string.Empty;
    public string? DES_ETIQUETA { get; set; }
}

/// <summary>PROC_ENTORNO_DICCIONARIO_ETIQUETAS_INSERT es upsert (UPDATE, y si 0 filas INSERT) —
/// el Web siempre llama POST, nunca PUT.</summary>
public class DiccionarioEtiquetasInsertDto
{
    [Required] public string COD_ETIQUETA { get; set; } = string.Empty;
    [Required] public string DES_ETIQUETA { get; set; } = string.Empty;
}
