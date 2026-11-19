using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad SOLICITUDES_COMERCIALES - tipos equivalentes a la BD.</summary>
public class SolicitudesComerciales
{
    public int NUM_SOLICITUD_COMERCIAL { get; set; }
    public string DES_SOLICITUD_COMERCIAL { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITUD_COMERCIAL { get; set; }
    public DateTime? FEC_REQUERIDA_ATENCION { get; set; }
    public DateTime? FEC_ASIGNACION_EFECTUADA { get; set; }
    public DateTime? FEC_ATENCION_EFECTUADA { get; set; }
    public string COD_TIPO_SOLICITUD_COMERCIAL { get; set; } = string.Empty;
    public decimal COD_ENTIDAD_CLIENTE { get; set; }
    public int? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    /// <summary>Asesor comercial reasignable vía PROC_COMERCIAL_SOLICITUDES_COMERCIALES_REASIGNAASESOR.</summary>
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    public string? DES_OBSERVACIONES { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_SOLICITUD_COMERICAL { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    // Alias del estado (ESTANDAR.ESTADOS): 'ANU' deja la solicitud en solo lectura.
    public string? COD_ESTADO_ALIAS { get; set; }
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO_RESPONSABLE { get; set; } = string.Empty;
    public string? DES_NOMBRE_COMPLETO_ASESOR_COMERCIAL { get; set; }
    public string DES_TEMPORADA { get; set; } = string.Empty;
    public string DES_COLECCION { get; set; } = string.Empty;

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

public class SolicitudesComercialesInsertDto
{
    // Sin [Required]: al crear se genera vía IDENTITY (SCOPE_IDENTITY()) dentro del propio SP INSERT.
    public int NUM_SOLICITUD_COMERCIAL { get; set; }
    [Required] public string DES_SOLICITUD_COMERCIAL { get; set; } = string.Empty;
    public DateTime? FEC_SOLICITUD_COMERCIAL { get; set; }
    public DateTime? FEC_REQUERIDA_ATENCION { get; set; }
    public DateTime? FEC_ASIGNACION_EFECTUADA { get; set; }
    public DateTime? FEC_ATENCION_EFECTUADA { get; set; }
    [Required] public string COD_TIPO_SOLICITUD_COMERCIAL { get; set; } = string.Empty;
    public decimal COD_ENTIDAD_CLIENTE { get; set; }
    public int? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? DES_OBSERVACIONES { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
}

public class SolicitudesComercialesSearchDto
{
    public string? Search { get; set; }
}

/// <summary>DTO para PROC_COMERCIAL_SOLICITUDES_COMERCIALES_ASIGNARRESPONSABLE — fija
/// Entidad Responsable + Observaciones y mueve el estado a "Asignada" (COD_ESTADO_ALIAS = 'ASG').</summary>
public class SolicitudesComercialesAsignarResponsableDto
{
    public int NUM_SOLICITUD_COMERCIAL { get; set; }
    [Required] public string COD_TIPO_SOLICITUD_COMERCIAL { get; set; } = string.Empty;
    [Required] public decimal COD_ENTIDAD_RESPONSABLE { get; set; }
    public string? DES_OBSERVACIONES { get; set; }
    [Required] public int COD_TIPO_ESTADO { get; set; }

    // Aplicación/opción desde donde se hace la asignación. El SP las reenvía a
    // PROC_COMERCIAL_SOLICITUDES_COMERCIALES_NOTIFICACION para registrar la notificación
    // contra la pantalla de origen.
    public string? COD_APLICACION_ORIGEN { get; set; }
    public string? COD_OPCION_APLICACION_ORIGEN { get; set; }
}

/// <summary>DTO para PROC_COMERCIAL_SOLICITUDES_COMERCIALES_REASIGNAASESOR — cambia el
/// COD_ENTIDAD_ASESOR_COMERCIAL de la solicitud; el SP genera notificaciones (asignación al
/// nuevo asesor, anulación al anterior) y no hace nada si el asesor es el mismo (ErrorNumber
/// 501: "EL NUEVO ASESOR COMERCIAL ES IDENTICO AL ANTERIOR").</summary>
public class SolicitudesComercialesReasignaAsesorDto
{
    [Required] public int NUM_SOLICITUD_COMERCIAL { get; set; }
    [Required] public decimal COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
}

/// <summary>Resultado de PROC_COMERCIAL_SOLICITUDES_LOADCOMBOS — un solo roundtrip para
/// los combos del diálogo en vez de llamadas separadas a Tipos de Solicitud y Clientes.</summary>
public class SolicitudesComercialesLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<ComboboxItem> TiposSolicitud { get; set; } = new();
    public List<ComboboxItem> Clientes { get; set; } = new();
    public List<EstadoItem> Estados { get; set; } = new();
    /// <summary>Entidades (todas) — antes se traía con una llamada HTTP aparte a
    /// EstandarRoute.EntidadesCombobox; ahora viene en el mismo LOADCOMBOS.</summary>
    public List<ComboboxItem> Entidades { get; set; } = new();
}
