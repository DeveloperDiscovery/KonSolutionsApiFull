using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposProductoRepository
{
    Task<IEnumerable<TiposProducto>> ShowallAsync();
    Task<TiposProducto?> ShowbyidAsync(string COD_TIPO_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposProducto>> SearchAsync(TiposProductoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposProductoInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PRODUCTO);
}

public interface ITiposProductoService
{
    Task<ApiResponse<IEnumerable<TiposProducto>>> GetAllAsync();
    Task<ApiResponse<TiposProducto>> GetByIdAsync(string COD_TIPO_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposProducto>>> SearchAsync(TiposProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposProductoInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PRODUCTO);
}
