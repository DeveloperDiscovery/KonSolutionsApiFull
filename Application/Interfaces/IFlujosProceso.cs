using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFlujosProcesoRepository
{
    Task<IEnumerable<FlujosProceso>> ShowallAsync();
    Task<FlujosProceso?> ShowbyidAsync(int NUM_FLUJO_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosProceso>> SearchAsync(FlujosProcesoSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosProcesoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosProcesoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO);
}

public interface IFlujosProcesoService
{
    Task<ApiResponse<IEnumerable<FlujosProceso>>> GetAllAsync();
    Task<ApiResponse<FlujosProceso>> GetByIdAsync(int NUM_FLUJO_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosProceso>>> SearchAsync(FlujosProcesoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO);
}