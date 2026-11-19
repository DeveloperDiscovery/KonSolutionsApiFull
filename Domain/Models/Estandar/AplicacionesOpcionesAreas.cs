namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.APLICACIONES_OPCIONES_AREAS - tipos equivalentes a la BD.
/// Clave compuesta COD_APLICACION + COD_OPCION_APLICACION + COD_AREA.</summary>
public class AplicacionesOpcionesAreas
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_AREA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class AplicacionesOpcionesAreasSearchDto
{
    public string? COD_APLICACION { get; set; }
    public string? Search { get; set; }
}

public class AplicacionesOpcionesAreasInsertDto
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_AREA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}
