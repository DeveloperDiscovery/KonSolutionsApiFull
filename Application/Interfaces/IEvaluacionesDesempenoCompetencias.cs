using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEvaluacionesDesempenoCompetenciasRepository
{
    Task<IEnumerable<EvaluacionesDesempenoCompetencias>> ShowallAsync();
    Task<EvaluacionesDesempenoCompetencias?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EvaluacionesDesempenoCompetencias>> SearchAsync(EvaluacionesDesempenoCompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(EvaluacionesDesempenoCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EvaluacionesDesempenoCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA);
}

public interface IEvaluacionesDesempenoCompetenciasService
{
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>> GetAllAsync();
    Task<ApiResponse<EvaluacionesDesempenoCompetencias>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoCompetencias>>> SearchAsync(EvaluacionesDesempenoCompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA, string COD_COMPETENCIA);
}