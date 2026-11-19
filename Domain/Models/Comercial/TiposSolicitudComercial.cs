using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_SOLICITUD_COMERCIAL - tipos equivalentes a la BD.</summary>
public class TiposSolicitudComercial
{
    public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    public string? DES_TIPO_SOLICITUD_COMERICAL { get; set; }
}

public class TiposSolicitudComercialInsertDto
{
    [Required] public string COD_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
}

public class TiposSolicitudComercialSearchDto
{
    public string? Search { get; set; }
}
