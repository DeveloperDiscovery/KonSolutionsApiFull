using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComponentesProductoRepository
{
    Task<IEnumerable<ComponentesProducto>> ShowallAsync();
    Task<ComponentesProducto?> ShowbyidAsync(string COD_COMPONENTE_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComponentesProducto>> SearchAsync(ComponentesProductoSearchDto dto);
    Task<WriteResult> InsertAsync(ComponentesProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ComponentesProductoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_COMPONENTE_PRODUCTO);
}

public interface IComponentesProductoService
{
    Task<ApiResponse<IEnumerable<ComponentesProducto>>> GetAllAsync();
    Task<ApiResponse<ComponentesProducto>> GetByIdAsync(string COD_COMPONENTE_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComponentesProducto>>> SearchAsync(ComponentesProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComponentesProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComponentesProductoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_COMPONENTE_PRODUCTO);
}