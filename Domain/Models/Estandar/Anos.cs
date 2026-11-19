using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.ANOS — NUM_ANO es IDENTITY (autogenerado por la BD al insertar,
/// PROC_ESTANDAR_ANOS_INSERT es upsert: intenta UPDATE por NUM_ANO y si no encuentra fila
/// hace INSERT ignorando el valor recibido, devolviendo el identity generado).</summary>
public class Anos
{
    public int NUM_ANO { get; set; }
    public string? DES_ANO { get; set; }
}

public class AnosInsertDto
{
    // 0/null en un alta nueva — la BD lo asigna vía IDENTITY (ver comentario de la entidad).
    public int NUM_ANO { get; set; }
    [Required] public string DES_ANO { get; set; } = string.Empty;
}
