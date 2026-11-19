using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS (LOGISTICA) - tipos equivalentes a la BD.
/// NUM_INVENTARIO es IDENTITY: la genera la base al insertar (ver InventariosRepository).</summary>
public class Inventarios
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public short NUM_INVENTARIO { get; set; }
    public string? DES_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? COD_TIPO_INVENTARIO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }

    // ── Solo lectura: traído por SEARCH/SHOWALL/SHOWBYID/SHOWCOMBOX vía JOIN ──
    public string? DES_NOMBRE_COMPLETO { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? DES_MOTIVO { get; set; }
    public string? DES_TIPO_INVENTARIO { get; set; }

    // ── Notificación en curso (ENTORNO.NOTIFICACIONES) ───────────────────────
    // Sólo lectura: la escribe el flujo de aprobación, no la pantalla. El documento
    // guarda el número y el resto viene por JOIN, incluidos los colores del estado,
    // que salen de ESTANDAR.ESTADOS como los de cualquier otro estado del sistema.
    public long?    NUM_NOTIFICACION            { get; set; }
    public string?  DES_NOTIFICACION            { get; set; }
    public DateTime? FEC_NOTIFICACION           { get; set; }
    public string?  DES_ESTADO_NOTIFICACION     { get; set; }
    public string?  DES_BACKCOLOR_NOTIFICACION  { get; set; }
    public string?  DES_FORECOLOR_NOTIFICACION  { get; set; }

}

public class InventariosInsertDto
{
    // NUM_INVENTARIO no se envía al crear: lo genera el SP (SCOPE_IDENTITY()).
    public string? DES_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? COD_TIPO_INVENTARIO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class InventariosUpdateDto
{
    [Required] public short NUM_INVENTARIO { get; set; }
    public string? DES_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? COD_TIPO_INVENTARIO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class InventariosSearchDto
{
    public string? Search { get; set; }
}
