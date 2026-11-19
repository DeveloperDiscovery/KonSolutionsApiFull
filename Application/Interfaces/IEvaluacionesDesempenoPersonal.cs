using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEvaluacionesDesempenoPersonalRepository
{
    Task<IEnumerable<EvaluacionesDesempenoPersonal>> ShowallAsync();
    Task<EvaluacionesDesempenoPersonal?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EvaluacionesDesempenoPersonal>> SearchAsync(EvaluacionesDesempenoPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(EvaluacionesDesempenoPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EvaluacionesDesempenoPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
}

public interface IEvaluacionesDesempenoPersonalService
{
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>> GetAllAsync();
    Task<ApiResponse<EvaluacionesDesempenoPersonal>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoPersonal>>> SearchAsync(EvaluacionesDesempenoPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, decimal COD_ENTIDAD_PERSONAL_EVALUADOR, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
}