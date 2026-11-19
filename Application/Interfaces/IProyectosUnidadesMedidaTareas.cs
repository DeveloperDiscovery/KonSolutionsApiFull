using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosUnidadesMedidaTareasRepository
{
    Task<IEnumerable<ProyectosUnidadesMedidaTareas>> ShowallAsync();
    Task<ProyectosUnidadesMedidaTareas?> ShowbyidAsync(string COD_UNIDAD_MEDIDA_TAREAS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosUnidadesMedidaTareas>> SearchAsync(ProyectosUnidadesMedidaTareasSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosUnidadesMedidaTareasInsertDto dto);
    Task<WriteResult> UpdateAsync(ProyectosUnidadesMedidaTareasUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_UNIDAD_MEDIDA_TAREAS);
}

public interface IProyectosUnidadesMedidaTareasService
{
    Task<ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>> GetAllAsync();
    Task<ApiResponse<ProyectosUnidadesMedidaTareas>> GetByIdAsync(string COD_UNIDAD_MEDIDA_TAREAS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosUnidadesMedidaTareas>>> SearchAsync(ProyectosUnidadesMedidaTareasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosUnidadesMedidaTareasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosUnidadesMedidaTareasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_UNIDAD_MEDIDA_TAREAS);
}