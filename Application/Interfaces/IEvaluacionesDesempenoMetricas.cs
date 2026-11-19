using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEvaluacionesDesempenoMetricasRepository
{
    Task<IEnumerable<EvaluacionesDesempenoMetricas>> ShowallAsync();
    Task<EvaluacionesDesempenoMetricas?> ShowbyidAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EvaluacionesDesempenoMetricas>> SearchAsync(EvaluacionesDesempenoMetricasSearchDto dto);
    Task<WriteResult> InsertAsync(EvaluacionesDesempenoMetricasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EvaluacionesDesempenoMetricasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
}

public interface IEvaluacionesDesempenoMetricasService
{
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>> GetAllAsync();
    Task<ApiResponse<EvaluacionesDesempenoMetricas>> GetByIdAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EvaluacionesDesempenoMetricas>>> SearchAsync(EvaluacionesDesempenoMetricasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EvaluacionesDesempenoMetricasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EvaluacionesDesempenoMetricasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EVALUACION_DESEMPENO, int COD_AREA, decimal COD_ENTIDAD_PERSONAL_EVALUADO);
}