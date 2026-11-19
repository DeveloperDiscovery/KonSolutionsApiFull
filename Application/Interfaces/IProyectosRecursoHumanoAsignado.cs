using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosRecursoHumanoAsignadoRepository
{
    Task<IEnumerable<ProyectosRecursoHumanoAsignado>> ShowallAsync();
    Task<ProyectosRecursoHumanoAsignado?> ShowbyidAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosRecursoHumanoAsignado>> SearchAsync(ProyectosRecursoHumanoAsignadoSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosRecursoHumanoAsignadoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosRecursoHumanoAsignadoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA);
}

public interface IProyectosRecursoHumanoAsignadoService
{
    Task<ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>> GetAllAsync();
    Task<ApiResponse<ProyectosRecursoHumanoAsignado>> GetByIdAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosRecursoHumanoAsignado>>> SearchAsync(ProyectosRecursoHumanoAsignadoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosRecursoHumanoAsignadoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosRecursoHumanoAsignadoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int COD_TAREA, decimal COD_ENTIDAD_ASIGNADA);
}