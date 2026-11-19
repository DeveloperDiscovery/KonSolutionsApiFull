using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad SEGURIDAD.ROLES_USUARIO_ALMACENES_TIPOS_MOVIMIENTO — qué tipos de
/// movimiento puede hacer un rol DENTRO de un almacén que ya tiene asignado.
/// Hija de SEGURIDAD.ROLES_USUARIO_ALMACENES.
/// PK: COD_ROL_USUARIO + COD_ALMACEN + COD_CLASE_MOVIMIENTO + COD_TIPO_MOVIMIENTO.
///
/// Los disponibles NO son todos los tipos del sistema: el SP _DISPONIBLES parte de
/// LOGISTICA.ALMACENES_TIPOS_MOVIMIENTO, o sea sólo los que ese almacén tiene habilitados.
/// Un rol no puede recibir permiso sobre un movimiento que el almacén no admite.</summary>
public class RolesUsuarioAlmacenesTiposMovimiento
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // Solo lectura (JOIN con ROLES_USUARIO, ALMACENES, CLASES_MOVIMIENTO y TIPOS_MOVIMIENTO).
    public string? DES_ROL_USUARIO { get; set; }
    public string? DES_ALMACEN { get; set; }
    public string? DES_ABREVIADA { get; set; }
    public string? DES_POLIGONO { get; set; }
    public string? DES_CLASE_MOVIMIENTO { get; set; }
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class RolesUsuarioAlmacenesTiposMovimientoInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
}

/// <summary>Lote de tipos. La clave del tipo es compuesta (clase + tipo), así que el lote no
/// puede ser una lista de códigos sueltos como en los otros hijos: se manda el par.</summary>
public class RolesUsuarioAlmacenesTiposMovimientoLoteDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public List<RolesUsuarioAlmacenesTiposMovimientoClave> Tipos { get; set; } = new();
}

/// <summary>Par clase+tipo. Existe porque un COD_TIPO_MOVIMIENTO sólo identifica un movimiento
/// dentro de su clase: el mismo código puede repetirse en clases distintas.</summary>
public class RolesUsuarioAlmacenesTiposMovimientoClave
{
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
}

public class RolesUsuarioAlmacenesTiposMovimientoSearchDto
{
    public string? COD_ROL_USUARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? Search { get; set; }
}

public class RolesUsuarioAlmacenesTiposMovimientoLoteResult
{
    public int Procesados { get; set; }
    public List<string> Fallidos { get; set; } = new();
    public string? DesError { get; set; }
}
