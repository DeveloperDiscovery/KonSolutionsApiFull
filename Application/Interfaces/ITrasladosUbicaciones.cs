using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITrasladosUbicacionesRepository
{
    Task<IEnumerable<TrasladoUbicacionesTipo>> TiposAsync(string codClaseMovimiento);
    Task<TrasladoUbicacionesMapa> MapaAsync(string codAlmacen);
    Task<IEnumerable<TrasladoUbicacionesStock>> StockAsync(string codAlmacen, string codUbicacion);
    Task<TrasladoUbicacionesResultado> RegistraAsync(TrasladoUbicacionesRegistraDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<TrasladoUbicacionesHistorial>> HistorialAsync(string codAlmacen, DateTime? desde, DateTime? hasta);
}

public interface ITrasladosUbicacionesService
{
    Task<ApiResponse<IEnumerable<TrasladoUbicacionesTipo>>> TiposAsync(string? codClaseMovimiento);
    Task<ApiResponse<TrasladoUbicacionesMapa>> MapaAsync(string? codAlmacen);
    Task<ApiResponse<IEnumerable<TrasladoUbicacionesStock>>> StockAsync(string? codAlmacen, string? codUbicacion);
    Task<ApiResponse<TrasladoUbicacionesResultado>> RegistraAsync(TrasladoUbicacionesRegistraDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<TrasladoUbicacionesHistorial>>> HistorialAsync(string? codAlmacen, DateTime? desde, DateTime? hasta);
}
