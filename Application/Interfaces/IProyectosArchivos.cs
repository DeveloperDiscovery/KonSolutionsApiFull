using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosArchivosRepository
{
    Task<IEnumerable<ProyectosArchivos>> ShowallAsync();
    Task<ProyectosArchivos?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosArchivos>> SearchAsync(ProyectosArchivosSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosArchivosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosArchivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}

public interface IProyectosArchivosService
{
    Task<ApiResponse<IEnumerable<ProyectosArchivos>>> GetAllAsync();
    Task<ApiResponse<ProyectosArchivos>> GetByIdAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosArchivos>>> SearchAsync(ProyectosArchivosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosArchivosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosArchivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}