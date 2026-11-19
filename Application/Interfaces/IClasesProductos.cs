using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesProductosRepository
{
    Task<IEnumerable<ClasesProductos>> ShowallAsync();
    Task<ClasesProductos?> ShowbyidAsync(string COD_CLASE_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesProductos>> SearchAsync(ClasesProductosSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesProductosInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesProductosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_PRODUCTO);
}

public interface IClasesProductosService
{
    Task<ApiResponse<IEnumerable<ClasesProductos>>> GetAllAsync();
    Task<ApiResponse<ClasesProductos>> GetByIdAsync(string COD_CLASE_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesProductos>>> SearchAsync(ClasesProductosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesProductosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesProductosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_PRODUCTO);
}