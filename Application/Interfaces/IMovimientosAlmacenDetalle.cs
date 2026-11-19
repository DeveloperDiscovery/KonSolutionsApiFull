using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenDetalleRepository
{
    Task<IEnumerable<MovimientosAlmacenDetalle>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenDetalle?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenDetalle>> SearchAsync(MovimientosAlmacenDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MovimientosAlmacenDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA);
    Task<IEnumerable<MovimientosAlmacenDetalleStockDisponible>> StockDisponibleAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenDetalleCombosLinea> CombosLineaAsync(decimal NUM_MOVIMIENTO_ALMACEN);

    // Lecturas de etiquetas (RFID / codigo de barras)
    Task<IEnumerable<MovimientosAlmacenEtiqueta>> EtiquetasAsync(decimal NUM_MOVIMIENTO_ALMACEN, int? NUM_SECUENCIA, string? search);
    Task<MovimientosAlmacenLecturaResultado> LeerAsync(MovimientosAlmacenLecturaDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> EtiquetaUpdateAsync(MovimientosAlmacenEtiquetaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> EtiquetaDeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA, string COD_STICKER_RFID, string codUsuario, string codEstacion);
    Task<IEnumerable<MovimientosAlmacenLecturaPendiente>> LecturasPendientesAsync(decimal NUM_MOVIMIENTO_ALMACEN);
}

public interface IMovimientosAlmacenDetalleService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenDetalle>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenDetalle>>> SearchAsync(MovimientosAlmacenDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosAlmacenDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<MovimientosAlmacenDetalleStockDisponible>>> StockDisponibleAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenDetalleCombosLinea>> CombosLineaAsync(decimal NUM_MOVIMIENTO_ALMACEN);

    // Lecturas de etiquetas (RFID / codigo de barras)
    Task<ApiResponse<IEnumerable<MovimientosAlmacenEtiqueta>>> EtiquetasAsync(decimal NUM_MOVIMIENTO_ALMACEN, int? NUM_SECUENCIA, string? search);
    Task<ApiResponse<MovimientosAlmacenLecturaResultado>> LeerAsync(MovimientosAlmacenLecturaDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> EtiquetaUpdateAsync(MovimientosAlmacenEtiquetaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> EtiquetaDeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, int NUM_SECUENCIA, string COD_STICKER_RFID, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<MovimientosAlmacenLecturaPendiente>>> LecturasPendientesAsync(decimal NUM_MOVIMIENTO_ALMACEN);
}