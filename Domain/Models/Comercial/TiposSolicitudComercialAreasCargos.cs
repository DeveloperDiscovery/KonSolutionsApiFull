using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS — tabla puente, nieta de
/// TiposSolicitudComercial (vía TiposSolicitudComercialAreas): qué cargos de cada área
/// participan en el flujo de aprobación de cada tipo de solicitud comercial.
/// PK compuesta (COD_TIPO_SOLICITUD_COMERICAL, COD_AREA, COD_CARGO).</summary>
public class TiposSolicitudComercialAreasCargos
{
    public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    public int?   COD_AREA { get; set; }
    public int?   COD_CARGO { get; set; }
    public int?   NUM_NIVEL_FLUJO { get; set; }
    public bool?  FLG_APROBADOR { get; set; }
    public bool?  FLG_NOTIFICADOR { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? DES_TIPO_SOLICITUD_COMERICAL { get; set; }
    public string? DES_AREA { get; set; }
    public string? DES_CARGO { get; set; }
}

public class TiposSolicitudComercialAreasCargosInsertDto
{
    [Required] public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    [Required] public int? COD_AREA { get; set; }
    [Required] public int? COD_CARGO { get; set; }
    public int? NUM_NIVEL_FLUJO { get; set; }
    public bool? FLG_APROBADOR { get; set; }
    public bool? FLG_NOTIFICADOR { get; set; }
}

public class TiposSolicitudComercialAreasCargosSearchDto
{
    [Required] public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    [Required] public int? COD_AREA { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_LOADCOMBOS:
/// Tipos de Solicitud Comercial + Cargos (universo completo, sin filtrar por área) en un solo
/// viaje para TiposSolicitudComercialAreasCargos.razor.</summary>
public class TiposSolicitudComercialAreasCargosLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> Tipos  { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Cargos { get; set; } = new();
}
