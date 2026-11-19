using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.FERIADOS: feriados, independientes de sábados y domingos. FLG_ANUAL = se repite cada año.</summary>
public class Feriados
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public DateTime FEC_FERIADO { get; set; }
    public string DES_FERIADO { get; set; } = string.Empty;
    public bool FLG_ANUAL { get; set; }
}

public class FeriadosInsertDto
{
    [Required] public DateTime FEC_FERIADO { get; set; }
    [Required] public string DES_FERIADO { get; set; } = string.Empty;
    public bool FLG_ANUAL { get; set; }
}

public class FeriadosUpdateDto
{
    [Required] public DateTime FEC_FERIADO { get; set; }
    [Required] public string DES_FERIADO { get; set; } = string.Empty;
    public bool FLG_ANUAL { get; set; }
}

public class FeriadosSearchDto
{
    public string? Search { get; set; }
}
