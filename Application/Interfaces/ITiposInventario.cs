using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposInventarioRepository
{
    Task<IEnumerable<TiposInventario>> ShowallAsync();
    Task<TiposInventario?> ShowbyidAsync(string COD_TIPO_INVENTARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposInventario>> SearchAsync(TiposInventarioSearchDto dto);
    Task<WriteResult> InsertAsync(TiposInventarioInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposInventarioUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_INVENTARIO);
}

public interface ITiposInventarioService
{
    Task<ApiResponse<IEnumerable<TiposInventario>>> GetAllAsync();
    Task<ApiResponse<TiposInventario>> GetByIdAsync(string COD_TIPO_INVENTARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposInventario>>> SearchAsync(TiposInventarioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposInventarioInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposInventarioUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_INVENTARIO);
}
