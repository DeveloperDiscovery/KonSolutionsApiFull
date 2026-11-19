using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

public class TiposNotificacion
{
    public string COD_TIPO_NOTIFICACION { get; set; } = string.Empty;
    public string? DES_TIPO_NOTIFICACION { get; set; }
    public bool? FLG_APROBACION { get; set; }
    public bool? FLG_CALENDARIO { get; set; }
    public bool? FLG_MENSAJE { get; set; }
    public short? COD_TIPO_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string? DES_TIPO_ESTADO { get; set; }
}

public class TiposNotificacionInsertDto
{
    [Required] public string COD_TIPO_NOTIFICACION { get; set; } = string.Empty;
    public string? DES_TIPO_NOTIFICACION { get; set; }
    public bool? FLG_APROBACION { get; set; }
    public bool? FLG_CALENDARIO { get; set; }
    public bool? FLG_MENSAJE { get; set; }
    public short? COD_TIPO_ESTADO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposNotificacionSearchDto
{
    public string? Search { get; set; }
}
