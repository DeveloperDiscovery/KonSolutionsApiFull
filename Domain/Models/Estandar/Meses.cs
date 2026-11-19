using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.MESES — NUM_MES es IDENTITY (autogenerado por la BD al insertar,
/// PROC_ESTANDAR_MESES_INSERT es upsert: intenta UPDATE por NUM_MES y si no encuentra fila
/// hace INSERT ignorando el valor recibido, devolviendo el identity generado).</summary>
public class Meses
{
    public int NUM_MES { get; set; }
    public string? DES_MES { get; set; }
    public int? NUM_DIAS { get; set; }
    public bool? FLG_AFECTA_BISIESTO { get; set; }
}

public class MesesInsertDto
{
    // 0/null en un alta nueva — la BD lo asigna vía IDENTITY (ver comentario de la entidad).
    public int NUM_MES { get; set; }
    [Required] public string DES_MES { get; set; } = string.Empty;
    public int? NUM_DIAS { get; set; }
    public bool? FLG_AFECTA_BISIESTO { get; set; }
}
