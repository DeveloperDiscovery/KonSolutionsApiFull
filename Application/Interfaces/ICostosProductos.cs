using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICostosProductosRepository
{
    Task<IEnumerable<CostosProductos>> ShowallAsync();
    Task<CostosProductos?> ShowbyidAsync(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CostosProductos>> SearchAsync(CostosProductosSearchDto dto);
    Task<WriteResult> InsertAsync(CostosProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CostosProductosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
}

public interface ICostosProductosService
{
    Task<ApiResponse<IEnumerable<CostosProductos>>> GetAllAsync();
    Task<ApiResponse<CostosProductos>> GetByIdAsync(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CostosProductos>>> SearchAsync(CostosProductosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CostosProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CostosProductosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
}