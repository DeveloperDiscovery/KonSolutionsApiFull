using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad SOLICITUDES_COMERCIALES_NOTIFICACIONES - notificaciones generadas por
/// una solicitud comercial. Solo lectura desde la UI ("Ver Aprobaciones"): las filas las
/// crea el propio flujo de notificación, no el usuario. PK: NUM_SOLICITUD_COMERCIAL +
/// NUM_NOTIFICACION. Los SP traen ya resueltos el tipo, estado y colores de ENTORNO.
/// NOTIFICACIONES / TIPOS_NOTIFICACION / ESTANDAR.ESTADOS.</summary>
public class SolicitudesComercialesNotificaciones
{
    public int NUM_SOLICITUD_COMERCIAL { get; set; }
    public int NUM_NOTIFICACION { get; set; }
    public string? DES_TIPO_NOTIFICACION { get; set; }
    public string? DES_INFORMACION { get; set; }
    public string? DES_NOTIFICACION { get; set; }
    public string? DES_OBSERVACIONES { get; set; }
    public short? COD_ESTADO { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

public class SolicitudesComercialesNotificacionesSearchDto
{
    [Required] public int NUM_SOLICITUD_COMERCIAL { get; set; }
    public string? Search { get; set; }
}
