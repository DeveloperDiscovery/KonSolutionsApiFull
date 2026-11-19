using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMovimientoTiposComprobantePagoRepository
{
    Task<IEnumerable<TiposMovimientoTiposComprobantePago>> ShowallAsync(string codClaseMovimiento, string codTipoMovimiento);
    Task<TiposMovimientoTiposComprobantePago?> ShowbyidAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codClaseMovimiento, string codTipoMovimiento);
    Task<IEnumerable<TiposMovimientoTiposComprobantePago>> SearchAsync(string codClaseMovimiento, string codTipoMovimiento, TiposMovimientoTiposComprobantePagoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO);
}

public interface ITiposMovimientoTiposComprobantePagoService
{
    Task<ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>> GetAllAsync(string codClaseMovimiento, string codTipoMovimiento);
    Task<ApiResponse<TiposMovimientoTiposComprobantePago>> GetByIdAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codClaseMovimiento, string codTipoMovimiento);
    Task<ApiResponse<IEnumerable<TiposMovimientoTiposComprobantePago>>> SearchAsync(string codClaseMovimiento, string codTipoMovimiento, TiposMovimientoTiposComprobantePagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposMovimientoTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO, string COD_TIPO_COMPROBANTE_PAGO);
}
