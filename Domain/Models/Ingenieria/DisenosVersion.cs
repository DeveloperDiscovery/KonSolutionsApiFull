using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad INGENIERIA.DISENOS_VERSION — tipos equivalentes a la BD.
/// Clave compuesta COD_DISENO + NUM_VERSION. Todos los SP (SHOWALL, SHOWCOMBOX,
/// SEARCH) filtran por @COD_DISENO: una versión solo existe dentro de un diseño.
/// COD_RUTA_PRODUCCION es nvarchar(100) en esta tabla (no el int de PRODUCCION_RUTAS).</summary>
public class DisenosVersion
{
    public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string? DES_VERSION { get; set; }
    public string? COD_CLASE_DESARROLLO { get; set; }
    public string? COD_TIPO_DESARROLLO { get; set; }
    public string? COD_RUTA_PRODUCCION { get; set; }
    public string? DES_OBSERVACIONES { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura (JOINs de los SP).
    public string? DES_DISENO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_ESTADO { get; set; }
    // Alias del estado (ESTANDAR.ESTADOS): 'ANU' deja la versión en solo lectura.
    public string? COD_ESTADO_ALIAS { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? DES_CLASE_DESARROLLO { get; set; }
    public string? DES_TIPO_DESARROLLO { get; set; }
    public string? DES_RUTA_PRODUCCION { get; set; }
}

/// <summary>DTO de guardado. PROC_INGENIERIA_DISENOS_VERSION_INSERT es un upsert
/// (intenta UPDATE por COD_DISENO+NUM_VERSION y, si no afecta filas, hace INSERT).</summary>
public class DisenosVersionInsertDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string? DES_VERSION { get; set; }
    public string? COD_CLASE_DESARROLLO { get; set; }
    public string? COD_TIPO_DESARROLLO { get; set; }
    public string? COD_RUTA_PRODUCCION { get; set; }
    public string? DES_OBSERVACIONES { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class DisenosVersionSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_INGENIERIA_DISENOS_VERSIONES_LOADCOMBOS — un solo roundtrip
/// para los combos del diálogo de versión (Clase de Desarrollo y Ruta de Producción).
/// UnidadesMedida viaja en la respuesta porque el SP la incluye, aunque el diálogo de
/// versión no tiene hoy un campo editable de unidad de medida.</summary>
public class DisenosVersionLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<ComboboxItem> ClasesDesarrollo { get; set; } = new();
    public List<ComboboxItem> ProduccionRutas { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida { get; set; } = new();
    public List<EstadoItem> Estados { get; set; } = new();
}
