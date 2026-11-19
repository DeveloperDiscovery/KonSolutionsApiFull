using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface ILiquidacionesComisionVentaRepository
{
    Task<IEnumerable<LiquidacionesComisionVenta>> ShowallAsync();
    Task<LiquidacionesComisionVenta?> ShowbyidAsync(int NUM_LIQUIDACION_COMISION_VENTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<LiquidacionesComisionVenta>> SearchAsync(LiquidacionesComisionVentaSearchDto dto);
    Task<WriteResult> InsertAsync(LiquidacionesComisionVentaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(LiquidacionesComisionVentaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA);
}

public interface ILiquidacionesComisionVentaService
{
    Task<ApiResponse<IEnumerable<LiquidacionesComisionVenta>>> GetAllAsync();
    Task<ApiResponse<LiquidacionesComisionVenta>> GetByIdAsync(int NUM_LIQUIDACION_COMISION_VENTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<LiquidacionesComisionVenta>>> SearchAsync(LiquidacionesComisionVentaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(LiquidacionesComisionVentaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(LiquidacionesComisionVentaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_LIQUIDACION_COMISION_VENTA);
}