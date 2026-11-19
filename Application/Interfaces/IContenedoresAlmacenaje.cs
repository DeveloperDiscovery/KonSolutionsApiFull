using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IContenedoresAlmacenajeRepository
{
    Task<IEnumerable<ContenedoresAlmacenaje>> ShowallAsync();
    Task<ContenedoresAlmacenaje?> ShowbyidAsync(string COD_CONTENEDOR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ContenedoresAlmacenaje>> SearchAsync(ContenedoresAlmacenajeSearchDto dto);
    Task<WriteResult> InsertAsync(ContenedoresAlmacenajeInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CONTENEDOR);
    Task<ContenedoresAlmacenajeLoadCombosResult> LoadcombosAsync();
}

public interface IContenedoresAlmacenajeService
{
    Task<ApiResponse<IEnumerable<ContenedoresAlmacenaje>>> GetAllAsync();
    Task<ApiResponse<ContenedoresAlmacenaje>> GetByIdAsync(string COD_CONTENEDOR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ContenedoresAlmacenaje>>> SearchAsync(ContenedoresAlmacenajeSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ContenedoresAlmacenajeInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CONTENEDOR);
    Task<ApiResponse<ContenedoresAlmacenajeLoadCombosResult>> LoadcombosAsync();
}
