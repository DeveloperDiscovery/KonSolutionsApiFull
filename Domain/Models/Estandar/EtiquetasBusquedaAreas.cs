using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ETIQUETAS_BUSQUEDA_AREAS - enlaza una Etiqueta de Búsqueda con el Área
/// responsable y los niveles jerárquicos (Gerencia/Sub Gerencia/Jefatura/Subjefatura/
/// Supervisor) a los que aplica. PK: COD_ETIQUETA_BUSQUEDA.</summary>
public class EtiquetasBusquedaAreas
{
    public string COD_ETIQUETA_BUSQUEDA { get; set; } = string.Empty;
    public byte? COD_AREA { get; set; }
    public bool? FLG_GERENCIA { get; set; }
    public bool? FLG_SUB_GERENCIA { get; set; }
    public bool? FLG_JEFATURA { get; set; }
    public bool? FLG_SUBJEFATURA { get; set; }
    public bool? FLG_SUPERVISOR { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

public class EtiquetasBusquedaAreasInsertDto
{
    [Required] public string COD_ETIQUETA_BUSQUEDA { get; set; } = string.Empty;
    public byte? COD_AREA { get; set; }
    public bool? FLG_GERENCIA { get; set; }
    public bool? FLG_SUB_GERENCIA { get; set; }
    public bool? FLG_JEFATURA { get; set; }
    public bool? FLG_SUBJEFATURA { get; set; }
    public bool? FLG_SUPERVISOR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}
