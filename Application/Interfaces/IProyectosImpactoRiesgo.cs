using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosImpactoRiesgoRepository
{
    Task<IEnumerable<ProyectosImpactoRiesgo>> ShowallAsync();
    Task<ProyectosImpactoRiesgo?> ShowbyidAsync(int COD_IMPACTO_RIESGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosImpactoRiesgo>> SearchAsync(ProyectosImpactoRiesgoSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosImpactoRiesgoInsertDto dto);
    Task<WriteResult> UpdateAsync(ProyectosImpactoRiesgoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_IMPACTO_RIESGO);
}

public interface IProyectosImpactoRiesgoService
{
    Task<ApiResponse<IEnumerable<ProyectosImpactoRiesgo>>> GetAllAsync();
    Task<ApiResponse<ProyectosImpactoRiesgo>> GetByIdAsync(int COD_IMPACTO_RIESGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosImpactoRiesgo>>> SearchAsync(ProyectosImpactoRiesgoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosImpactoRiesgoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosImpactoRiesgoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_IMPACTO_RIESGO);
}