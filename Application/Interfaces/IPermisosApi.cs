using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPermisosApiRepository
{
    /// <summary>Todos los permisos del rol, de una sola vez. No hay versión "por entidad" a
    /// propósito: el filtro corre en cada request y una consulta por request sería el mayor
    /// costo del sistema. Se trae completo y se cachea.</summary>
    Task<IEnumerable<PermisoApi>> ShowallAsync(string COD_ROL_USUARIO);

    /// <summary>¿La clave que viene en el cuerpo del POST ya existe en esa tabla?
    /// true = el POST es una modificación · false = es un alta · null = no se pudo determinar.</summary>
    Task<bool?> ExisteRegistroAsync(string DES_OBJETO, string json);

    /// <summary>Estado actual del registro identificado por DES_OBJETO+json, para que el
    /// guardián de flujo (FlujoAuthorizationFilter) pueda validar contra
    /// OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA sin que cada entidad tenga que exponer su
    /// propio "dime tu estado". (null, null) = no se pudo determinar — tabla inexistente, sin
    /// las columnas de estado, sin PK, o el registro no existe.</summary>
    Task<(short? CodTipoEstado, short? CodEstado)> EstadoActualAsync(string DES_OBJETO, string json);

    /// <summary>Deja rastro del intento rechazado (o del que se habría rechazado en modo
    /// auditoría). Nunca debe tumbar el request: si la auditoría falla, se traga el error.</summary>
    Task AuditarAsync(string codUsuario, string codRol, string desObjeto, string verbo, string ruta, string estacion);
}

public interface IPermisosApiService
{
    /// <summary>Resuelve si el rol puede ejecutar ese verbo sobre esa entidad.
    /// Para POST consulta si la clave existe, y según eso exige el permiso de alta o el de
    /// modificación — los SP _INSERT hacen upsert y el verbo solo no alcanza para saberlo.</summary>
    Task<bool> PuedeAsync(string codRol, string desObjeto, string verbo, string? jsonCuerpo);

    /// <summary>Descarta el caché de un rol. Hay que llamarlo cuando cambian los permisos,
    /// si no el usuario sigue con los viejos hasta que venza el TTL.</summary>
    void Invalidar(string? codRol = null);
}
