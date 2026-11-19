using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Solicitudes;

/// <summary>Entidad TIPOS_SOLICITUD - tipos equivalentes a la BD.</summary>
public class TiposSolicitud
{
    public string COD_TIPO_SOLICITUD { get; set; } = string.Empty;
    public string DES_TIPO_SOLICITUD { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
}

public class TiposSolicitudInsertDto
{
    [Required] public string COD_TIPO_SOLICITUD { get; set; } = string.Empty;
    [Required] public string DES_TIPO_SOLICITUD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposSolicitudUpdateDto
{
    [Required] public string COD_TIPO_SOLICITUD { get; set; } = string.Empty;
    [Required] public string DES_TIPO_SOLICITUD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposSolicitudSearchDto
{
    public string? Search { get; set; }
}