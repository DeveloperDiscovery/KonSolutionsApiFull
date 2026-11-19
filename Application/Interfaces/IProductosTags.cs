using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProductosTagsRepository
{
    Task<IEnumerable<ProductosTags>> ShowallAsync();
    Task<ProductosTags?> ShowbyidAsync(string COD_PRODUCTO_TAG);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProductosTags>> SearchAsync(ProductosTagsSearchDto dto);
    Task<WriteResult> InsertAsync(ProductosTagsInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProductosTagsUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PRODUCTO_TAG);
}

public interface IProductosTagsService
{
    Task<ApiResponse<IEnumerable<ProductosTags>>> GetAllAsync();
    Task<ApiResponse<ProductosTags>> GetByIdAsync(string COD_PRODUCTO_TAG);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProductosTags>>> SearchAsync(ProductosTagsSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProductosTagsInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProductosTagsUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO_TAG);
}