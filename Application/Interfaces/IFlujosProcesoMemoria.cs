using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFlujosProcesoMemoriaRepository
{
    Task<IEnumerable<FlujosProcesoMemoria>> ShowallAsync();
    Task<FlujosProcesoMemoria?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosProcesoMemoria>> SearchAsync(FlujosProcesoMemoriaSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosProcesoMemoriaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosProcesoMemoriaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
}

public interface IFlujosProcesoMemoriaService
{
    Task<ApiResponse<IEnumerable<FlujosProcesoMemoria>>> GetAllAsync();
    Task<ApiResponse<FlujosProcesoMemoria>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosProcesoMemoria>>> SearchAsync(FlujosProcesoMemoriaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoMemoriaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoMemoriaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
}