using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.USUARIOS_TIPOS_MOVIMIENTO_DIRECT_ACCESS — los tipos de
/// movimiento que un usuario marcó como acceso directo, para el documento de Movimientos
/// de Almacén. La clave es (COD_USUARIO, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO): un
/// mismo tipo puede repetirse entre usuarios, pero no dos veces para el mismo usuario.
///
/// DES_TIPO_MOVIMIENTO e IMG_ICONO llegan del JOIN contra LOGISTICA.TIPOS_MOVIMIENTO: son
/// del maestro, no de esta tabla. Anulables porque el JOIN es LEFT — si el tipo se borró
/// después de marcarlo como acceso directo, la fila igual aparece, sin nombre ni ícono.</summary>
public class UsuariosTiposMovimientoDirectAccess
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string? DES_CORREO_INSTITUCIONAL { get; set; }
    public string? DES_USUARIO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class UsuariosTiposMovimientoDirectAccessInsertDto
{
    // SIN [Required]: el controlador lo pisa con el usuario logueado (GetCodUsuario())
    // ANTES de ejecutar el insert, pero DESPUÉS de que ModelState ya se validó contra lo
    // que mandó el cliente — que nunca trae este campo, a propósito, para que nadie pueda
    // marcar un acceso directo a nombre de otro con sólo tocar el cuerpo del POST. Con
    // [Required] acá, esa validación fallaba antes de llegar a la línea que lo corrige.
    public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
}

public class UsuariosTiposMovimientoDirectAccessSearchDto
{
    public string? Search { get; set; }
}
