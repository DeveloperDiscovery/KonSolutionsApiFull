using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.OPCIONES_PROCESOS_ESTADOS - tipos equivalentes a la BD.
/// Clave compuesta COD_APLICACION + COD_OPCION_APLICACION + COD_TIPO_ESTADO. Asigna qué
/// Tipos de Estado puede usar cada Aplicación+Opción del sistema. Tabla de asignación pura:
/// el SP _INSERT hace upsert internamente, no hay SP _UPDATE.</summary>
public class OpcionesProcesosEstados
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    public string? DES_TIPO_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? DES_FORMULARIO { get; set; }
    public string? DES_OBJETO { get; set; }
    public string? DES_OPCION_APLICACION { get; set; }
    public string? DES_PARAMETRO { get; set; }
}

public class OpcionesProcesosEstadosInsertDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public short COD_TIPO_ESTADO { get; set; }
    // Nullable a propósito: el Controller los completa con GetCodUsuario()/GetEstacion()
    // DESPUÉS de ModelState.IsValid — si fueran no-nulables, ASP.NET Core exige
    // implícitamente que vengan informados en el body y la validación falla antes de
    // que el Controller tenga oportunidad de rellenarlos.
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class OpcionesProcesosEstadosSearchDto
{
    public string? COD_APLICACION { get; set; }
    public string? COD_OPCION_APLICACION { get; set; }
    public string? Search { get; set; }
}
