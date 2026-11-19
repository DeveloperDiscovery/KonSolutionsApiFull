using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface ILiquidacionesComisionVentaDetalleRepository
{
    Task<IEnumerable<LiquidacionesComisionVentaDetalle>> ShowallAsync();
    Task<LiquidacionesComisionVentaDetalle?> ShowbyidAsync(int NUM_LIQUIDACION_COMISION_VENTA, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<LiquidacionesComisionVentaDetalle>> SearchAsync(LiquidacionesComisionVentaDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(LiquidacionesComisionVentaDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(LiquidacionesComisionVentaDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA, int NUM_SECUENCIA);
}

public interface ILiquidacionesComisionVentaDetalleService
{
    Task<ApiResponse<IEnumerable<LiquidacionesComisionVentaDetalle>>> GetAllAsync();
    Task<ApiResponse<LiquidacionesComisionVentaDetalle>> GetByIdAsync(int NUM_LIQUIDACION_COMISION_VENTA, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<LiquidacionesComisionVentaDetalle>>> SearchAsync(LiquidacionesComisionVentaDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(LiquidacionesComisionVentaDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(LiquidacionesComisionVentaDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA, int NUM_SECUENCIA);
}