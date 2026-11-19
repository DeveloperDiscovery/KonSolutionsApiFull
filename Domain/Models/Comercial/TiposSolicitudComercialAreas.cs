using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_SOLICITUD_COMERCIAL_AREAS — tabla puente, hija de
/// TiposSolicitudComercial: qué áreas administrativas participan de cada tipo de
/// solicitud comercial. PK compuesta (COD_TIPO_SOLICITUD_COMERICAL, COD_AREA).</summary>
public class TiposSolicitudComercialAreas
{
    public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    public int?   COD_AREA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? DES_TIPO_SOLICITUD_COMERICAL { get; set; }
    public string? DES_AREA { get; set; }
}

public class TiposSolicitudComercialAreasInsertDto
{
    [Required] public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    [Required] public int? COD_AREA { get; set; }
}

public class TiposSolicitudComercialAreasSearchDto
{
    [Required] public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    public string? Search { get; set; }
}
