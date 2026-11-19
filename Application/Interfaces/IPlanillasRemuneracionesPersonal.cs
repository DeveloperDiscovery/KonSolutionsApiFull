using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPlanillasRemuneracionesPersonalRepository
{
    Task<IEnumerable<PlanillasRemuneracionesPersonal>> ShowallAsync();
    Task<PlanillasRemuneracionesPersonal?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PlanillasRemuneracionesPersonal>> SearchAsync(PlanillasRemuneracionesPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(PlanillasRemuneracionesPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PlanillasRemuneracionesPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
}

public interface IPlanillasRemuneracionesPersonalService
{
    Task<ApiResponse<IEnumerable<PlanillasRemuneracionesPersonal>>> GetAllAsync();
    Task<ApiResponse<PlanillasRemuneracionesPersonal>> GetByIdAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PlanillasRemuneracionesPersonal>>> SearchAsync(PlanillasRemuneracionesPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PlanillasRemuneracionesPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PlanillasRemuneracionesPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL);
}