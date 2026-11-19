using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFlujosProcesoDocumentacionRepository
{
    Task<IEnumerable<FlujosProcesoDocumentacion>> ShowallAsync();
    Task<FlujosProcesoDocumentacion?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosProcesoDocumentacion>> SearchAsync(FlujosProcesoDocumentacionSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosProcesoDocumentacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosProcesoDocumentacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
}

public interface IFlujosProcesoDocumentacionService
{
    Task<ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>> GetAllAsync();
    Task<ApiResponse<FlujosProcesoDocumentacion>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosProcesoDocumentacion>>> SearchAsync(FlujosProcesoDocumentacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoDocumentacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoDocumentacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA);
}