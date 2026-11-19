using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoMovimientosAlmacenRepository
{
    Task<IEnumerable<ComprobantesPagoMovimientosAlmacen>> ShowallAsync();
    Task<ComprobantesPagoMovimientosAlmacen?> ShowbyidAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoMovimientosAlmacen>> SearchAsync(ComprobantesPagoMovimientosAlmacenSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN);
}

public interface IComprobantesPagoMovimientosAlmacenService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoMovimientosAlmacen>> GetByIdAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoMovimientosAlmacen>>> SearchAsync(ComprobantesPagoMovimientosAlmacenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA_COMPROBANTE, decimal NUM_MOVIMIENTO_ALMACEN);
}