using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPlanillasRemuneracionesDetalleRepository
{
    Task<IEnumerable<PlanillasRemuneracionesDetalle>> ShowallAsync();
    Task<PlanillasRemuneracionesDetalle?> ShowbyidAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PlanillasRemuneracionesDetalle>> SearchAsync(PlanillasRemuneracionesDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(PlanillasRemuneracionesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PlanillasRemuneracionesDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
}

public interface IPlanillasRemuneracionesDetalleService
{
    Task<ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>> GetAllAsync();
    Task<ApiResponse<PlanillasRemuneracionesDetalle>> GetByIdAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PlanillasRemuneracionesDetalle>>> SearchAsync(PlanillasRemuneracionesDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PlanillasRemuneracionesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PlanillasRemuneracionesDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PLANILLA_REMUNERATIVA, decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
}