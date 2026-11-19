using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCatalogoPreciosRepository
{
    Task<IEnumerable<TiposCatalogoPrecios>> ShowallAsync();
    Task<TiposCatalogoPrecios?> ShowbyidAsync(string COD_TIPO_CATALOGO_PRECIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCatalogoPrecios>> SearchAsync(TiposCatalogoPreciosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCatalogoPreciosInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCatalogoPreciosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CATALOGO_PRECIO);
}

public interface ITiposCatalogoPreciosService
{
    Task<ApiResponse<IEnumerable<TiposCatalogoPrecios>>> GetAllAsync();
    Task<ApiResponse<TiposCatalogoPrecios>> GetByIdAsync(string COD_TIPO_CATALOGO_PRECIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCatalogoPrecios>>> SearchAsync(TiposCatalogoPreciosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCatalogoPreciosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCatalogoPreciosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CATALOGO_PRECIO);
}