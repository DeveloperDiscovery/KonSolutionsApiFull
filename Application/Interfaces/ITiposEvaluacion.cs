using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEvaluacionRepository
{
    Task<IEnumerable<TiposEvaluacion>> ShowallAsync();
    Task<TiposEvaluacion?> ShowbyidAsync(string COD_TIPO_EVALUACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposEvaluacion>> SearchAsync(TiposEvaluacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEvaluacionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposEvaluacionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_EVALUACION);
}

public interface ITiposEvaluacionService
{
    Task<ApiResponse<IEnumerable<TiposEvaluacion>>> GetAllAsync();
    Task<ApiResponse<TiposEvaluacion>> GetByIdAsync(string COD_TIPO_EVALUACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposEvaluacion>>> SearchAsync(TiposEvaluacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEvaluacionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEvaluacionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_EVALUACION);
}