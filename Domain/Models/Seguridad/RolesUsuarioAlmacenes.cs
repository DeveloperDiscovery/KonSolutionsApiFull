using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad SEGURIDAD.ROLES_USUARIO_ALMACENES — qué almacenes puede operar un rol.
/// Hija de SEGURIDAD.ROLES_USUARIO. PK compuesta: COD_ROL_USUARIO + COD_ALMACEN.
///
/// Es una tabla de asignación pura: fuera de la auditoría no tiene datos propios, así que la
/// pantalla no es un CRUD con formulario sino un asignar/desasignar entre dos listas.</summary>
public class RolesUsuarioAlmacenes
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura (JOIN con ROLES_USUARIO y LOGISTICA.ALMACENES).
    public string? DES_ROL_USUARIO { get; set; }
    public string? DES_ALMACEN { get; set; }
    public string? DES_ABREVIADA { get; set; }
    public string? DES_POLIGONO { get; set; }
}

/// <summary>El SP _INSERT hace upsert por la PK. Usuario y estación salen del token.</summary>
public class RolesUsuarioAlmacenesInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
}

/// <summary>Asignación o baja EN BLOQUE. Va como una sola llamada y no como N: marcar quince
/// almacenes y disparar quince peticiones deja el rol a medio configurar si alguna falla por
/// el medio, y sin forma de saber cuáles entraron.</summary>
public class RolesUsuarioAlmacenesLoteDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public List<string> Almacenes { get; set; } = new();
}

public class RolesUsuarioAlmacenesSearchDto
{
    public string? COD_ROL_USUARIO { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de una operación en bloque. Se informa el detalle porque en un lote
/// parcial el usuario necesita saber exactamente qué quedó afuera — y por qué.</summary>
public class RolesUsuarioAlmacenesLoteResult
{
    public int Procesados { get; set; }
    public List<string> Fallidos { get; set; } = new();
    public string? DesError { get; set; }
}
