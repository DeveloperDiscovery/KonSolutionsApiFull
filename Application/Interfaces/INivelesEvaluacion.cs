using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface INivelesEvaluacionRepository
{
    Task<IEnumerable<NivelesEvaluacion>> ShowallAsync();
    Task<NivelesEvaluacion?> ShowbyidAsync(int NUM_NIVEL_EVALUACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<NivelesEvaluacion>> SearchAsync(NivelesEvaluacionSearchDto dto);
    Task<WriteResult> InsertAsync(NivelesEvaluacionInsertDto dto);
    Task<WriteResult> UpdateAsync(NivelesEvaluacionUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_NIVEL_EVALUACION);
}

public interface INivelesEvaluacionService
{
    Task<ApiResponse<IEnumerable<NivelesEvaluacion>>> GetAllAsync();
    Task<ApiResponse<NivelesEvaluacion>> GetByIdAsync(int NUM_NIVEL_EVALUACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<NivelesEvaluacion>>> SearchAsync(NivelesEvaluacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(NivelesEvaluacionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(NivelesEvaluacionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_NIVEL_EVALUACION);
}