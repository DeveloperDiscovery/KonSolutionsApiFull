using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosRepository
{
    Task<IEnumerable<Proyectos>> ShowallAsync();
    Task<Proyectos?> ShowbyidAsync(int NUM_PROYECTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Proyectos>> SearchAsync(ProyectosSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO);
}

public interface IProyectosService
{
    Task<ApiResponse<IEnumerable<Proyectos>>> GetAllAsync();
    Task<ApiResponse<Proyectos>> GetByIdAsync(int NUM_PROYECTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Proyectos>>> SearchAsync(ProyectosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO);
}
