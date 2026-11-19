using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRequerimientosStockRepository
{
    // Los filtros son OPCIONALES: una llamada sin nada sigue devolviendo el listado completo,
    // como siempre. Hay otras pantallas que piden este listado sin filtrar.
    Task<IEnumerable<RequerimientosStock>> ShowallAsync(RequerimientosStockSearchDto? filtros = null);
    Task<RequerimientosStock?> ShowbyidAsync(int NUM_REQUERIMIENTO_STOCK);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RequerimientosStock>> SearchAsync(RequerimientosStockSearchDto dto);
    Task<WriteResult> InsertAsync(RequerimientosStockInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RequerimientosStockUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_REQUERIMIENTO_STOCK);
    Task<RequerimientosStockLoadCombosResult> LoadcombosAsync();
    Task<WriteResult> MakereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion);
    Task<WriteResult> ReversereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion);
}

public interface IRequerimientosStockService
{
    Task<ApiResponse<IEnumerable<RequerimientosStock>>> GetAllAsync(RequerimientosStockSearchDto? filtros = null);
    Task<ApiResponse<RequerimientosStock>> GetByIdAsync(int NUM_REQUERIMIENTO_STOCK);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RequerimientosStock>>> SearchAsync(RequerimientosStockSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RequerimientosStockInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RequerimientosStockUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_REQUERIMIENTO_STOCK);
    Task<ApiResponse<RequerimientosStockLoadCombosResult>> LoadcombosAsync();
    Task<ApiResponse<WriteResult>> MakereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> ReversereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion);
}