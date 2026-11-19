using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEvaluacionesDesempenoTiposCompetenciasRepository
{
    Task<IEnumerable<EvaluacionesDesempenoTiposCompetencias>> ShowallAsync();
    Task<EvaluacionesDesempenoTiposCompetencias?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EvaluacionesDesempenoTiposCompetencias>> SearchAsync(EvaluacionesDesempenoTiposCompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(EvaluacionesDesempenoTiposCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EvaluacionesDesempenoTiposCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA);
}

public interface IEvaluacionesDesempenoTiposCompetenciasService
{
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>> GetAllAsync();
    Task<ApiResponse<EvaluacionesDesempenoTiposCompetencias>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoTiposCompetencias>>> SearchAsync(EvaluacionesDesempenoTiposCompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoTiposCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoTiposCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, string COD_TIPO_COMPETENCIA);
}