using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosProbabilidadRiesgoRepository
{
    Task<IEnumerable<ProyectosProbabilidadRiesgo>> ShowallAsync();
    Task<ProyectosProbabilidadRiesgo?> ShowbyidAsync(int COD_PROBABILIDAD_RIESGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosProbabilidadRiesgo>> SearchAsync(ProyectosProbabilidadRiesgoSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosProbabilidadRiesgoInsertDto dto);
    Task<WriteResult> UpdateAsync(ProyectosProbabilidadRiesgoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_PROBABILIDAD_RIESGO);
}

public interface IProyectosProbabilidadRiesgoService
{
    Task<ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>> GetAllAsync();
    Task<ApiResponse<ProyectosProbabilidadRiesgo>> GetByIdAsync(int COD_PROBABILIDAD_RIESGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosProbabilidadRiesgo>>> SearchAsync(ProyectosProbabilidadRiesgoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosProbabilidadRiesgoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosProbabilidadRiesgoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_PROBABILIDAD_RIESGO);
}