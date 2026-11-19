using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosTareasRepository
{
    Task<IEnumerable<ProyectosTareas>> ShowallAsync();
    Task<ProyectosTareas?> ShowbyidAsync(int NUM_PROYECTO, int COD_TAREA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosTareas>> SearchAsync(ProyectosTareasSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosTareasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosTareasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_TAREA);
}

public interface IProyectosTareasService
{
    Task<ApiResponse<IEnumerable<ProyectosTareas>>> GetAllAsync();
    Task<ApiResponse<ProyectosTareas>> GetByIdAsync(int NUM_PROYECTO, int COD_TAREA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosTareas>>> SearchAsync(ProyectosTareasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosTareasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosTareasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int COD_TAREA);
}