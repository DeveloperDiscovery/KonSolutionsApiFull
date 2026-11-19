using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosSubTareasRepository
{
    Task<IEnumerable<ProyectosSubTareas>> ShowallAsync();
    Task<ProyectosSubTareas?> ShowbyidAsync(int NUM_PROYECTO, int COD_TAREA, int COD_SUB_TAREA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosSubTareas>> SearchAsync(ProyectosSubTareasSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosSubTareasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosSubTareasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, int COD_SUB_TAREA);
}

public interface IProyectosSubTareasService
{
    Task<ApiResponse<IEnumerable<ProyectosSubTareas>>> GetAllAsync();
    Task<ApiResponse<ProyectosSubTareas>> GetByIdAsync(int NUM_PROYECTO, int COD_TAREA, int COD_SUB_TAREA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosSubTareas>>> SearchAsync(ProyectosSubTareasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosSubTareasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosSubTareasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, int COD_SUB_TAREA);
}