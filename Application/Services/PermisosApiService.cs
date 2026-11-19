using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using Microsoft.Extensions.Caching.Memory;

namespace KonSolutionsApi.Application.Services;

public class PermisosApiService : IPermisosApiService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PermisosApiService);

    /// <summary>Media hora. Era de cinco minutos cuando la única forma de refrescar era esperar
    /// el vencimiento; desde que InvalidacionPermisosFilter descarta el caché apenas alguien
    /// toca las tablas de permisos, el TTL dejó de ser el mecanismo de actualización y pasó a
    /// ser sólo una red de seguridad. Alargarlo saca consultas de encima sin perder frescura.
    ///
    /// El único caso que sigue dependiendo del vencimiento es que alguien edite los permisos
    /// directamente en la base, sin pasar por la API.</summary>
    private static readonly TimeSpan Vigencia = TimeSpan.FromMinutes(30);

    private readonly IPermisosApiRepository _repo;
    private readonly IMemoryCache _cache;
    private readonly ErrorLogService _log;

    /// <summary>Se lleva la lista de claves cacheadas porque IMemoryCache no permite
    /// enumerarlas: sin esto, Invalidar() sin rol no tendría a quién sacar.</summary>
    private static readonly HashSet<string> _rolesCacheados = new();
    private static readonly object _candado = new();

    public PermisosApiService(IPermisosApiRepository repo, IMemoryCache cache, ErrorLogService log)
    {
        _repo = repo; _cache = cache; _log = log;
    }

    public async Task<bool> PuedeAsync(string codRol, string desObjeto, string verbo, string? jsonCuerpo)
    {
        var permisos = await ObtenerAsync(codRol);

        // Sin fila para el objeto, el rol no tiene ninguna opción que la alcance.
        if (!permisos.TryGetValue(desObjeto, out var permiso)) return false;

        // El upsert es el único caso que el verbo no resuelve solo.
        if (!verbo.Equals("POST", StringComparison.OrdinalIgnoreCase))
            return permiso.Permite(verbo);

        var existe = await ExisteAsync(desObjeto, jsonCuerpo);
        return existe switch
        {
            true => permiso.FLG_ACCION_PUT,     // ya estaba: es modificación
            false => permiso.FLG_ACCION_POST,   // no estaba: es alta
            // No se pudo determinar (tabla sin PK, objeto desconocido, cuerpo raro).
            // Alcanza con tener cualquiera de los dos: negar acá bloquearía a alguien que sí
            // tiene permiso, y este camino es la excepción, no la regla.
            _ => permiso.FLG_ACCION_POST || permiso.FLG_ACCION_PUT
        };
    }

    private async Task<bool?> ExisteAsync(string desObjeto, string? jsonCuerpo)
    {
        if (string.IsNullOrWhiteSpace(jsonCuerpo)) return null;
        try
        {
            return await _repo.ExisteRegistroAsync(desObjeto, jsonCuerpo);
        }
        catch (Exception ex)
        {
            // Que el desempate falle no puede tumbar el request: se degrada a "no sé".
            _log.LogError(ex, Programa, Clase, nameof(ExisteAsync));
            return null;
        }
    }

    /// <summary>Trae los permisos del rol, cacheados. Ante un error devuelve un diccionario
    /// vacío en vez de propagar: quién decide qué hacer con "no hay permisos" es el filtro,
    /// que según el modo configurado audita o bloquea.</summary>
    private async Task<Dictionary<string, PermisoApi>> ObtenerAsync(string codRol)
    {
        var clave = $"permisos-api:{codRol}";
        if (_cache.TryGetValue(clave, out Dictionary<string, PermisoApi>? cacheado) && cacheado is not null)
            return cacheado;

        Dictionary<string, PermisoApi> mapa;
        try
        {
            var filas = await _repo.ShowallAsync(codRol);
            mapa = filas
                .Where(p => !string.IsNullOrWhiteSpace(p.DES_OBJETO))
                // El SP ya agrupa por objeto, pero un DES_OBJETO cargado con distinta
                // capitalización llegaría como dos filas: se unifica acá.
                .GroupBy(p => p.DES_OBJETO.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, Combinar, StringComparer.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ObtenerAsync));
            return new Dictionary<string, PermisoApi>(StringComparer.OrdinalIgnoreCase);
        }

        _cache.Set(clave, mapa, Vigencia);
        lock (_candado) _rolesCacheados.Add(clave);
        return mapa;
    }

    /// <summary>Suma de verbos: si el rol llega al objeto por dos opciones distintas, vale
    /// lo que le permita cualquiera de las dos.</summary>
    private static PermisoApi Combinar(IGrouping<string, PermisoApi> g) => new()
    {
        DES_OBJETO = g.Key,
        FLG_ACCION_GET = g.Any(x => x.FLG_ACCION_GET),
        FLG_ACCION_POST = g.Any(x => x.FLG_ACCION_POST),
        FLG_ACCION_PUT = g.Any(x => x.FLG_ACCION_PUT),
        FLG_ACCION_DELETE = g.Any(x => x.FLG_ACCION_DELETE)
    };

    public void Invalidar(string? codRol = null)
    {
        lock (_candado)
        {
            if (!string.IsNullOrWhiteSpace(codRol))
            {
                var clave = $"permisos-api:{codRol}";
                _cache.Remove(clave);
                _rolesCacheados.Remove(clave);
                return;
            }
            foreach (var c in _rolesCacheados) _cache.Remove(c);
            _rolesCacheados.Clear();
        }
    }
}
