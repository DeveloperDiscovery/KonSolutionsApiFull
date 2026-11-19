using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesProcesoRepository
{
    Task<IEnumerable<ClasesProceso>> ShowallAsync();
    Task<ClasesProceso?> ShowbyidAsync(string COD_CLASE_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesProceso>> SearchAsync(ClasesProcesoSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesProcesoInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesProcesoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_PROCESO);
}

public interface IClasesProcesoService
{
    Task<ApiResponse<IEnumerable<ClasesProceso>>> GetAllAsync();
    Task<ApiResponse<ClasesProceso>> GetByIdAsync(string COD_CLASE_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesProceso>>> SearchAsync(ClasesProcesoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesProcesoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesProcesoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_PROCESO);
}