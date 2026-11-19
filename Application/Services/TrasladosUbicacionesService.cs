using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

/// <summary>Traslados entre ubicaciones de un mismo almacén: el mapa de racks, el stock de una
/// ubicación, el registro del traslado y su historial.</summary>
public class TrasladosUbicacionesService : ITrasladosUbicacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TrasladosUbicacionesService);
    private readonly ITrasladosUbicacionesRepository _repo;
    private readonly ErrorLogService _log;
    public TrasladosUbicacionesService(ITrasladosUbicacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TrasladoUbicacionesTipo>>> TiposAsync(string? codClaseMovimiento)
    {
        if (string.IsNullOrWhiteSpace(codClaseMovimiento))
            return ApiResponse<IEnumerable<TrasladoUbicacionesTipo>>.Fail("Falta la clase de movimiento.");
        try { var d = (await _repo.TiposAsync(codClaseMovimiento.Trim())).ToList();
              return ApiResponse<IEnumerable<TrasladoUbicacionesTipo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TiposAsync));
              return ApiResponse<IEnumerable<TrasladoUbicacionesTipo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TrasladoUbicacionesMapa>> MapaAsync(string? codAlmacen)
    {
        if (string.IsNullOrWhiteSpace(codAlmacen)) return ApiResponse<TrasladoUbicacionesMapa>.Fail("Elige el almacén.");
        try { return ApiResponse<TrasladoUbicacionesMapa>.Ok(await _repo.MapaAsync(codAlmacen.Trim())); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(MapaAsync));
              return ApiResponse<TrasladoUbicacionesMapa>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TrasladoUbicacionesStock>>> StockAsync(string? codAlmacen, string? codUbicacion)
    {
        if (string.IsNullOrWhiteSpace(codAlmacen) || string.IsNullOrWhiteSpace(codUbicacion))
            return ApiResponse<IEnumerable<TrasladoUbicacionesStock>>.Fail("Falta el almacén o la ubicación.");
        try { var d = (await _repo.StockAsync(codAlmacen.Trim(), codUbicacion.Trim())).ToList();
              return ApiResponse<IEnumerable<TrasladoUbicacionesStock>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(StockAsync));
              return ApiResponse<IEnumerable<TrasladoUbicacionesStock>>.Fail(ex.Message); }
    }

    /// <summary>Lo básico se valida acá para responder rápido; el resto (disponible, ubicaciones
    /// del almacén, tipo de movimiento) lo valida el SP dentro de la transacción, que es donde vale.
    /// Los errores del SP (THROW 503xx) ya vienen redactados para el usuario.</summary>
    public async Task<ApiResponse<TrasladoUbicacionesResultado>> RegistraAsync(TrasladoUbicacionesRegistraDto dto, string codUsuario, string codEstacion)
    {
        if (string.IsNullOrWhiteSpace(dto.COD_ALMACEN)) return ApiResponse<TrasladoUbicacionesResultado>.Fail("Elige el almacén.");
        if (string.IsNullOrWhiteSpace(dto.COD_TIPO_MOVIMIENTO)) return ApiResponse<TrasladoUbicacionesResultado>.Fail("Elige el tipo de movimiento.");
        if (dto.Lineas is not { Count: > 0 }) return ApiResponse<TrasladoUbicacionesResultado>.Fail("El traslado no tiene líneas.");
        if (dto.Lineas.Any(l => !(l.CAN_PRODUCTO > 0))) return ApiResponse<TrasladoUbicacionesResultado>.Fail("Todas las líneas tienen que tener cantidad.");
        if (dto.Lineas.Any(l => string.Equals(l.COD_UBICACION_ORIGEN?.Trim(), l.COD_UBICACION_DESTINO?.Trim(), StringComparison.OrdinalIgnoreCase)))
            return ApiResponse<TrasladoUbicacionesResultado>.Fail("Hay líneas con el mismo origen y destino.");
        try
        {
            var r = await _repo.RegistraAsync(dto, codUsuario, codEstacion);
            return ApiResponse<TrasladoUbicacionesResultado>.Ok(r);
        }
        catch (SqlException ex) when (ex.Number is >= 50000)
        {
            // Validación del SP: el mensaje es para el usuario, no hace falta el log de errores.
            return ApiResponse<TrasladoUbicacionesResultado>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(RegistraAsync));
            return ApiResponse<TrasladoUbicacionesResultado>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<TrasladoUbicacionesHistorial>>> HistorialAsync(string? codAlmacen, DateTime? desde, DateTime? hasta)
    {
        if (string.IsNullOrWhiteSpace(codAlmacen)) return ApiResponse<IEnumerable<TrasladoUbicacionesHistorial>>.Fail("Elige el almacén.");
        try { var d = (await _repo.HistorialAsync(codAlmacen.Trim(), desde, hasta)).ToList();
              return ApiResponse<IEnumerable<TrasladoUbicacionesHistorial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(HistorialAsync));
              return ApiResponse<IEnumerable<TrasladoUbicacionesHistorial>>.Fail(ex.Message); }
    }
}
