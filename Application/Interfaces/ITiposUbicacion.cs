using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

// Sin SearchAsync: esta tabla no tiene SP _SEARCH (el export trae 5 SP, no 6).
// El filtrado se hace en el cliente sobre el listado completo, que es un catálogo corto.
public interface ITiposUbicacionRepository
{
    Task<IEnumerable<TiposUbicacion>> ShowallAsync();
    Task<TiposUbicacion?> ShowbyidAsync(string COD_TIPO_UBICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(TiposUbicacionInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_UBICACION);
}

public interface ITiposUbicacionService
{
    Task<ApiResponse<IEnumerable<TiposUbicacion>>> GetAllAsync();
    Task<ApiResponse<TiposUbicacion>> GetByIdAsync(string COD_TIPO_UBICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(TiposUbicacionInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_UBICACION);
}
