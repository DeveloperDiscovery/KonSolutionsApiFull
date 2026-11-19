using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEvaluacionesDesempenoPersonalEvaluacionRepository
{
    Task<IEnumerable<EvaluacionesDesempenoPersonalEvaluacion>> ShowallAsync();
    Task<EvaluacionesDesempenoPersonalEvaluacion?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EvaluacionesDesempenoPersonalEvaluacion>> SearchAsync(EvaluacionesDesempenoPersonalEvaluacionSearchDto dto);
    Task<WriteResult> InsertAsync(EvaluacionesDesempenoPersonalEvaluacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EvaluacionesDesempenoPersonalEvaluacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
}

public interface IEvaluacionesDesempenoPersonalEvaluacionService
{
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoPersonalEvaluacion>>> GetAllAsync();
    Task<ApiResponse<EvaluacionesDesempenoPersonalEvaluacion>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoPersonalEvaluacion>>> SearchAsync(EvaluacionesDesempenoPersonalEvaluacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoPersonalEvaluacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoPersonalEvaluacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
}