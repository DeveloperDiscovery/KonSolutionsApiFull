using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

public class TablasTiposEstado
{
    public string  DES_TABLA       { get; set; } = string.Empty;
    public int?    COD_TIPO_ESTADO { get; set; }

    // Viene por JOIN a ESTANDAR.TIPOS_ESTADOS (Showall/Showcombox/Search) — no lo trae Showbyid.
    public string? DES_TIPO_ESTADO { get; set; }
}

public class TablasTiposEstadoInsertDto
{
    [Required] public string DES_TABLA { get; set; } = string.Empty;
    public int COD_TIPO_ESTADO { get; set; }
}

public class TablasTiposEstadoSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Fila de PROC_ENTORNO_TABLAS_TIPOS_ESTADO_TABLASDISPONIBLES: tablas del sistema
/// que todavía NO están configuradas en ESTANDAR.TABLAS_TIPOS_ESTADO.</summary>
public class TablaDisponible
{
    public string NombreTabla { get; set; } = string.Empty;
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public long? FilasEstimadas { get; set; }
}
