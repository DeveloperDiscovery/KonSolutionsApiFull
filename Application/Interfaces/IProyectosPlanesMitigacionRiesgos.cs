using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosPlanesMitigacionRiesgosRepository
{
    Task<IEnumerable<ProyectosPlanesMitigacionRiesgos>> ShowallAsync();
    Task<ProyectosPlanesMitigacionRiesgos?> ShowbyidAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosPlanesMitigacionRiesgos>> SearchAsync(ProyectosPlanesMitigacionRiesgosSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosPlanesMitigacionRiesgosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosPlanesMitigacionRiesgosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION);
}

public interface IProyectosPlanesMitigacionRiesgosService
{
    Task<ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>> GetAllAsync();
    Task<ApiResponse<ProyectosPlanesMitigacionRiesgos>> GetByIdAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosPlanesMitigacionRiesgos>>> SearchAsync(ProyectosPlanesMitigacionRiesgosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosPlanesMitigacionRiesgosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosPlanesMitigacionRiesgosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, string COD_PLAN_MITIGACION);
}