using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFlujosProcesoEtapasRepository
{
    Task<IEnumerable<FlujosProcesoEtapas>> ShowallAsync();
    Task<FlujosProcesoEtapas?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosProcesoEtapas>> SearchAsync(FlujosProcesoEtapasSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosProcesoEtapasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosProcesoEtapasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA);
}

public interface IFlujosProcesoEtapasService
{
    Task<ApiResponse<IEnumerable<FlujosProcesoEtapas>>> GetAllAsync();
    Task<ApiResponse<FlujosProcesoEtapas>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosProcesoEtapas>>> SearchAsync(FlujosProcesoEtapasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoEtapasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoEtapasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA);
}