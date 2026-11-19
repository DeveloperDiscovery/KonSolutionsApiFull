using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPlanillaRemuneracionesRepository
{
    Task<IEnumerable<PlanillaRemuneraciones>> ShowallAsync();
    Task<PlanillaRemuneraciones?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PlanillaRemuneraciones>> SearchAsync(PlanillaRemuneracionesSearchDto dto);
    Task<WriteResult> InsertAsync(PlanillaRemuneracionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PlanillaRemuneracionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA);
}

public interface IPlanillaRemuneracionesService
{
    Task<ApiResponse<IEnumerable<PlanillaRemuneraciones>>> GetAllAsync();
    Task<ApiResponse<PlanillaRemuneraciones>> GetByIdAsync(int NUM_PLANILLA_REMUNERATIVA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PlanillaRemuneraciones>>> SearchAsync(PlanillaRemuneracionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PlanillaRemuneracionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PlanillaRemuneracionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA);
}