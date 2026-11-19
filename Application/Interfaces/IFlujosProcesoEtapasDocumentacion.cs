using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFlujosProcesoEtapasDocumentacionRepository
{
    Task<IEnumerable<FlujosProcesoEtapasDocumentacion>> ShowallAsync();
    Task<FlujosProcesoEtapasDocumentacion?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosProcesoEtapasDocumentacion>> SearchAsync(FlujosProcesoEtapasDocumentacionSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosProcesoEtapasDocumentacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosProcesoEtapasDocumentacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA);
}

public interface IFlujosProcesoEtapasDocumentacionService
{
    Task<ApiResponse<IEnumerable<FlujosProcesoEtapasDocumentacion>>> GetAllAsync();
    Task<ApiResponse<FlujosProcesoEtapasDocumentacion>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosProcesoEtapasDocumentacion>>> SearchAsync(FlujosProcesoEtapasDocumentacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoEtapasDocumentacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoEtapasDocumentacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA);
}