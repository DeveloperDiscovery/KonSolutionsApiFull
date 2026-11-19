using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPlanillasRemuneracionesFormulasRepository
{
    Task<IEnumerable<PlanillasRemuneracionesFormulas>> ShowallAsync();
    Task<PlanillasRemuneracionesFormulas?> ShowbyidAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PlanillasRemuneracionesFormulas>> SearchAsync(PlanillasRemuneracionesFormulasSearchDto dto);
    Task<WriteResult> InsertAsync(PlanillasRemuneracionesFormulasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PlanillasRemuneracionesFormulasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA);
}

public interface IPlanillasRemuneracionesFormulasService
{
    Task<ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>> GetAllAsync();
    Task<ApiResponse<PlanillasRemuneracionesFormulas>> GetByIdAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PlanillasRemuneracionesFormulas>>> SearchAsync(PlanillasRemuneracionesFormulasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PlanillasRemuneracionesFormulasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PlanillasRemuneracionesFormulasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA_FORMULA);
}