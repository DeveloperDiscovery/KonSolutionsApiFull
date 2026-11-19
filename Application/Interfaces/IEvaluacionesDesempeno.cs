using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEvaluacionesDesempenoRepository
{
    Task<IEnumerable<EvaluacionesDesempeno>> ShowallAsync();
    Task<EvaluacionesDesempeno?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EvaluacionesDesempeno>> SearchAsync(EvaluacionesDesempenoSearchDto dto);
    Task<WriteResult> InsertAsync(EvaluacionesDesempenoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EvaluacionesDesempenoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO);
}

public interface IEvaluacionesDesempenoService
{
    Task<ApiResponse<IEnumerable<EvaluacionesDesempeno>>> GetAllAsync();
    Task<ApiResponse<EvaluacionesDesempeno>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EvaluacionesDesempeno>>> SearchAsync(EvaluacionesDesempenoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO);
}