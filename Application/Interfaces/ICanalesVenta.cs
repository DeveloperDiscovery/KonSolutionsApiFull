using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICanalesVentaRepository
{
    Task<IEnumerable<CanalesVenta>> ShowallAsync();
    Task<CanalesVenta?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CanalesVenta>> SearchAsync(CanalesVentaSearchDto dto);
    Task<WriteResult> InsertAsync(CanalesVentaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CanalesVentaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA);
}

public interface ICanalesVentaService
{
    Task<ApiResponse<IEnumerable<CanalesVenta>>> GetAllAsync();
    Task<ApiResponse<CanalesVenta>> GetByIdAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CanalesVenta>>> SearchAsync(CanalesVentaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CanalesVentaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CanalesVentaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA);
}