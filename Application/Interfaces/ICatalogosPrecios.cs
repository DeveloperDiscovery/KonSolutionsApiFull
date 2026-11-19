using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICatalogosPreciosRepository
{
    Task<IEnumerable<CatalogosPrecios>> ShowallAsync();
    Task<CatalogosPrecios?> ShowbyidAsync(int NUM_CATALOGO_PRECIOS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CatalogosPrecios>> SearchAsync(CatalogosPreciosSearchDto dto);
    Task<WriteResult> InsertAsync(CatalogosPreciosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CatalogosPreciosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CATALOGO_PRECIOS);
}

public interface ICatalogosPreciosService
{
    Task<ApiResponse<IEnumerable<CatalogosPrecios>>> GetAllAsync();
    Task<ApiResponse<CatalogosPrecios>> GetByIdAsync(int NUM_CATALOGO_PRECIOS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CatalogosPrecios>>> SearchAsync(CatalogosPreciosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CatalogosPreciosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CatalogosPreciosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CATALOGO_PRECIOS);
}