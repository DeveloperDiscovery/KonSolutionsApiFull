using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfirmacionRecepcionRepository
{
    Task<IEnumerable<RecepcionPendiente>> PendientesAsync(string codRol, string codUsuario, string? codAlmacen);
    Task<IEnumerable<RecepcionLinea>> DetalleAsync(decimal numMovimiento);
    Task<RecepcionMotivosRechazo> MotivosRechazoAsync(decimal numMovimiento);
    Task<RecepcionResultado> ConfirmarAsync(RecepcionConfirmarDto dto, string codUsuario, string codEstacion);
    Task<RecepcionResultado> RechazarAsync(RecepcionRechazarDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<RecepcionLecturaLinea>> LecturaContextoAsync(decimal numMovimiento);
    Task<IEnumerable<MovimientosAlmacenEtiqueta>> EtiquetasAsync(decimal numMovimiento, int? numSecuencia);
    Task<RecepcionLecturaResultado> LeerAsync(RecepcionLecturaDto dto);
}

public interface IConfirmacionRecepcionService
{
    Task<ApiResponse<IEnumerable<RecepcionPendiente>>> PendientesAsync(string? codRol, string codUsuario, string? codAlmacen);
    Task<ApiResponse<IEnumerable<RecepcionLinea>>> DetalleAsync(decimal numMovimiento);
    Task<ApiResponse<RecepcionMotivosRechazo>> MotivosRechazoAsync(decimal numMovimiento);
    Task<ApiResponse<RecepcionResultado>> ConfirmarAsync(RecepcionConfirmarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RecepcionResultado>> RechazarAsync(RecepcionRechazarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<RecepcionLecturaLinea>>> LecturaContextoAsync(decimal numMovimiento);
    Task<ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>> EtiquetasAsync(decimal numMovimiento, int? numSecuencia);
    Task<ApiResponse<RecepcionLecturaResultado>> LeerAsync(RecepcionLecturaDto dto);
}
